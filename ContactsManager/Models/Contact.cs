using ContactsManager.Helpers;
using ContactsManager.Interfaces;

namespace ContactsManager.Models
{
    public class Contact : BindableBase
    {

        public Contact()
        {
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        //private string _email;
        //public string Email
        //{
        //    get => _email;
        //    set => SetProperty(ref _email, value);
        //}

        //private GenderType _gender;
        //public GenderType Gender
        //{
        //    get => _gender;
        //    set => SetProperty(ref _gender, value);
        //}
    }
}