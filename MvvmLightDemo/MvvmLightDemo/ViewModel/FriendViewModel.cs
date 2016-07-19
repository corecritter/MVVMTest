using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MvvmLightDemo.Message;
using MvvmLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightDemo.ViewModel
{
    public class FriendViewModel : ViewModelBase
    {
        public Friend Model
        {
            get;
            private set;
        }
        public FriendViewModel(Friend model, FullNameSchema schema)
        {
            Model = model;
            Model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Friend.FirstNamePropertyName || e.PropertyName == Friend.LastNamePropertyName)
                {
                    RaisePropertyChanged(() => FullName);
                    return;
                }
                if (e.PropertyName == Friend.DateOfBirthPropertyName)
                {
                    RaisePropertyChanged(() => DateOfBirthFormatted);
                }
            };
            Messenger.Default.Register<ChangeFullNameMessage>(
              this,
              msg =>
              {
                  //_schema = msg.Schema;
                  RaisePropertyChanged(() => FullName);
              });
        }
        public string DateOfBirthFormatted
        {
            get
            {
                return Model.DateOfBirth.ToString("d");
            }
        }
        public string FullName
        {
            get
            {
                return String.Format("{0}, {1}", Model.FirstName, Model.LastName);
                //switch (_schema)
                //{
                //    case FullNameSchema.LastFirstComma:
                //        return string.Format(
                //          "{0}, {1}",
                //          Model.LastName, Model.FirstName);
                //    default:
                //        return string.Format(
                //          "{0} {1}",
                //          Model.FirstName, Model.LastName);
                //}
            }
        }
    }
}
