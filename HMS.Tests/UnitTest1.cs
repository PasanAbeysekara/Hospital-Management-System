using FluentAssertions;
using HMS.MVVM.Model;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using HMS.MVVM.ViewModel;
using System.Globalization;
using System.Security.RightsManagement;

namespace HMS.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Calculate_Correct_Doctor_Fee_For_Given_Patient()
        {
            var doctors = new List<Doctor>{
                new Doctor{ Id=1 , Name = "TestDoctor_1" , Fee = 100 },
				new Doctor{ Id=2 , Name = "TestDoctor_2" , Fee = 200 },
				new Doctor{ Id=3 , Name = "TestDoctor_3" , Fee = 300 },
			};

            var appointments = new List<Appointment>
            {
                new Appointment{ Id = 1 ,AppointedDate = DateTime.Now.AddDays(4) , DoctorId = 1, PatientId = 1},
				new Appointment{ Id = 2 ,AppointedDate = DateTime.Now.AddDays(5) , DoctorId = 2, PatientId = 1},
				new Appointment{ Id = 3 ,AppointedDate = DateTime.Now.AddDays(6) , DoctorId = 3, PatientId = 1},
			};

            // calculate doctor fee for patient with PatientId is 1
            var calculatedDoctorFee = PatientProfileVM.calculateDoctorFeeBasedOnAppointments(appointments,doctors);

            // should return 100+200+300 = 600 
            calculatedDoctorFee.Should().Be(600);

        }

        [Fact]
        public void Should_Calculate_Correct_Test_Fee_For_Given_Patient()
        {
            var tests = new List<Test>
            {
                new Test{ Id = 1 ,TestName="TestName_1",Fee=200,Description="TestDescription_1"},
				new Test{ Id = 2 ,TestName="TestName_2",Fee=200,Description="TestDescription_2"},
				new Test{ Id = 3 ,TestName="TestName_3",Fee=300,Description="TestDescription_3"},
			};

            var prescriptions = new List<Prescription>
            {
                new Prescription { Id=1 , PatientId=1 , PrescribedDate = DateTime.Now.AddDays(2) },
				new Prescription { Id=2 , PatientId=1 , PrescribedDate = DateTime.Now.AddDays(3) },
				new Prescription { Id=3 , PatientId=1 , PrescribedDate = DateTime.Now.AddDays(4) },
			};

            var medicalTests = new List<MedicalTest> 
            {
                new MedicalTest{ Id = 1 , PrescriptionId=1, TestId=1, Description="medicalTestDescription_1"},
				new MedicalTest{ Id = 2 , PrescriptionId=2, TestId=2, Description="medicalTestDescription_1"},
				new MedicalTest{ Id = 3 , PrescriptionId=3, TestId=3, Description="medicalTestDescription_1"},
			};


			// calculate test fee for patient with PatientId is 1
			var calculatedTestFee =  PatientProfileVM.calculateTestFeeBasedOnPrescription(prescriptions, medicalTests, tests);

            // should return 200+200+300 = 700
            calculatedTestFee.Should().Be(700);

		}

        [Fact]
        public void Should_Calculate_Correct_Hospital_Fee_For_Given_Patient()
        {
            // DoctorFee and TestFee of patient with PatientId is 1
            var doctorFee = 600;
            var testFee = 700;

			// calculate hospital fee for patient with PatientId is 1
            // (Here hospital fee is 10% of total of doctor fee and test fee)
			var calculatedHospitalFee = PatientProfileVM.calculateHospitalFee(doctorFee, testFee);

            // should return (600+700) x 10 % = 130
            calculatedHospitalFee.Should().Be(130);
		}

        [Fact]
		public void Should_Calculate_Correct_Total_Fee_For_Given_Patient()
        {
			// DoctorFee and TestFee of patient with PatientId is 1
			var doctorFee = 600;
			var testFee = 700;

			// calculate total fee for patient with PatientId is 1
			// (Here total fee is total of doctor fee , test fee & hospital fee )
			var calculatedTotalFee = PatientProfileVM.calculateTotalFee(doctorFee, testFee);

			// should return 600 + 700 + (600+700) x 10 % = 1430
			calculatedTotalFee.Should().Be(1430);

		}




	}
}