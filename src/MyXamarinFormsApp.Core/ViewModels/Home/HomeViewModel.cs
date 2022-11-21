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

            InitCollectionView();
        }

        private async void InitCollectionView()
        {
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student("Sachin", "Chavan", i, "image_placeholder.png"));
            }
            await Task.Delay(5000);
            Students.Clear();
            IsBusy = false;
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student("Sachin", "Chavan", i, "image_placeholder.png"));
            }

        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public Student(string firstName, string lastName, int id, string imageUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            ImageUrl = imageUrl;
        }
    }
}
