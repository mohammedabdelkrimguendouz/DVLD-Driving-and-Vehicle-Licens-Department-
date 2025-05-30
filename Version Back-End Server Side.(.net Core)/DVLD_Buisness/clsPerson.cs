﻿using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0,Update = 1  };
        public enMode Mode = enMode.AddNew;

        public enum enGender { Male=0,Female=1};
        public PersonDTO personDTO
        {
            get
            {
                return new PersonDTO(this.PersonID, this.NationalNo, this.Gender, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.ImagePath, this.NationalityCountryID);
            }
        }

        public object AllPersonInfo
        {
            get
            {
                return new
                {
                    personID = this.PersonID,
                    nationalNo = this.NationalNo,
                    gender = this.Gender,
                    firstName = this.FirstName,
                    secondName = this.SecondName,
                    thirdName = this.ThirdName,
                    lastName = this.LastName,
                    email = this.Email,
                    phone = this.Phone,
                    address = this.Address,
                    dateOfBirth = this.DateOfBirth,
                    imagePath = this.ImagePath,
                    countryInfo = this.CountryInfo.countryDTO
                };
            }
        }
        

        public int PersonID { set; get; }
        public string NationalNo { set; get; }

        public byte Gender { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }

        public string ThirdName { set; get; }

        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }



        public string ImagePath { set; get; }

        public int NationalityCountryID { set; get; }

        public clsCountry CountryInfo;
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        

        public clsPerson(PersonDTO personDTO , enMode CreationMode=enMode.AddNew)

        {
            this.PersonID = personDTO.PersonID;
            this.NationalNo = personDTO.NationalNo;
            this.FirstName = personDTO.FirstName;
            this.SecondName = personDTO.SecondName;
            this.ThirdName = personDTO.ThirdName;
            this.LastName = personDTO.LastName;
            this.Email = personDTO.Email;
            this.Phone = personDTO.Phone;
            this.Address = personDTO.Address;
            this.Gender = personDTO.Gender;
            this.DateOfBirth = personDTO.DateOfBirth;
            this.NationalityCountryID = personDTO.NationalityCountryID;
            this.ImagePath = personDTO.ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = CreationMode;
        }



        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.personDTO);
            return (this.PersonID != -1);
        }

        public static clsPerson Find(int PersonID)
        {

            PersonDTO personDTO = clsPersonData.GetPersonInfoByID(PersonID);
            if (personDTO!=null)
            {
                return new clsPerson(personDTO,enMode.Update);

            }
            return null;
        }

        public static clsPerson Find(string NationalNo)
        {

           
            PersonDTO personDTO = clsPersonData.GetPersonInfoByNationalNo(NationalNo);
            if (personDTO != null)
            {
                return new clsPerson(personDTO, enMode.Update);

            }
            return null;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.personDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
      

        static public List<PeopleListDTO> GetAllPeople()
        {
            return clsPersonData.GetAllPeople() ;

        }

        static public bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExistByID(PersonID);
        }
        static public bool ISPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExistByNationalNo(NationalNo);
        }
    }
}
