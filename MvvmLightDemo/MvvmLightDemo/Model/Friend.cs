using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightDemo.Model
{
    //Inheriting ObservableObject allows Friend Class to easily raise the PropertyChanged event
    public class Friend : ObservableObject
    {
        public Friend()
        {
            
        }
        /// <summary>
        /// The <see cref="FirstName" /> property's name.
        /// </summary>
        public const string FirstNamePropertyName = "FirstName";

        private string _firstName = String.Empty;

        /// <summary>
        /// Sets and gets the FirstName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                Set(FirstNamePropertyName, ref _firstName, value);
            }
        }

        /// <summary>
            /// The <see cref="LastName" /> property's name.
            /// </summary>
        public const string LastNamePropertyName = "LastName";

        private string _lastName = String.Empty;

        /// <summary>
        /// Sets and gets the LastName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                Set(LastNamePropertyName, ref _lastName, value);
            }
        }
        /// <summary>
        /// The <see cref="DateOfBirthString" /> property's name.
        /// </summary>
        public const string DateOfBirthStringPropertyName = "DateOfBirthString";

        private string _dateOfBirthString = String.Empty;

        /// <summary>
        /// Sets and gets the DateOfBirthString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DateOfBirthString
        {
            get
            {
                return _dateOfBirthString;
            }
            set
            {
                if(Set(DateOfBirthStringPropertyName, ref _dateOfBirthString, value)) //Returns true if the value changed
                {
                    RaisePropertyChanged(() => DateOfBirth); //Forces DataBindings to requery and reformat with DateOfBirth Property
                }

            }
        }

        public const string DateOfBirthPropertyName = "DateOfBirth";

        //TO convert DOB string to proper date/time
        public DateTime DateOfBirth
        {
            get
            {
                if (string.IsNullOrEmpty(_dateOfBirthString))
                {
                    return DateTime.MinValue;
                }
                return DateTime.ParseExact(DateOfBirthString, "d",
                  CultureInfo.InvariantCulture);
            }
            set
            {
                _dateOfBirthString = value.ToString("d",
                  CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
            /// The <see cref="ImageUrl" /> property's name.
            /// </summary>
        public const string ImageUrlPropertyName = "ImageUrl";

        private string _imageUrl = String.Empty;

        /// <summary>
        /// Sets and gets the ImageUrl property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return _imageUrl ;
            }
            set
            {
                Set(ImageUrlPropertyName, ref _imageUrl , value);
            }
        }
    }
}
