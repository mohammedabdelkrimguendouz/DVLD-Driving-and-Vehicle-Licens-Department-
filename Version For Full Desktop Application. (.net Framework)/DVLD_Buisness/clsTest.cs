﻿using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }

       
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo;

        public string Notes {  get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public bool TestResult { get; set; }
        



        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.Notes = "";
            this.TestResult = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsTest(int TestID, int TestAppointmentID,
             bool TestResult, string Notes, int CreatedByUserID)
        {

            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.Notes = Notes;
            this.TestResult = TestResult;
            this.CreatedByUserID =CreatedByUserID ;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            string Notes="";
            bool TestResult = false;
            if (clsTestData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult,
                ref Notes, ref CreatedByUserID))
                return new clsTest(TestID,  TestAppointmentID,
                TestResult,  Notes, CreatedByUserID);
            return null;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,
             this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID,this.TestAppointmentID,
             this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTest();

            }
            return false;
        }

        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 4;
        }
    }

}
