using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MyXamarinFormsApp.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students { get => _students; set => SetProperty(ref _students, value); }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }  
        public HomeViewModel()
        {
            Students = new ObservableCollection<Student>();
            IsBusy = true;
        }

        public override async Task Initialize()
        {
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student("Sachin", "Chavan", i, "image_placeholder.png",true));
            }
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBusy { get; set; }

        public Student(string firstName, string lastName, int id, string imageUrl, bool isBusy)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            ImageUrl = imageUrl;
            IsBusy = isBusy;
        }
    }
}
