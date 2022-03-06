using ClassLibrary1;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{

    public partial class HomeController : Controller
    {
        //private readonly PatrickDBEntities db = new PatrickDBEntities();
        private readonly ph13269705671_PatrickDBEntities webDB = new ph13269705671_PatrickDBEntities();

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Programe()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public virtual ActionResult Tution()
        {
            //ViewBag.GetAllStudents = db.GetAllStudentsInfo().ToList();
            return View();
        }

        //public virtual ActionResult GetAllStudentsInfo()
        //{
        //    return Json(db.GetAllStudentsInfo());
        //}

        public virtual ActionResult Comment()
        {
            return View();
        }

        public virtual ActionResult TimeTable()
        {
            ViewBag.show = "false";
            return View();
        }

        //[HttpPost]
        //public virtual ActionResult TimeTable(PatientInfoModel patient)
        //{
        //    var getPatientInfo = GetAllPatientsInfoByPatientID(patient);
        //    var getPatientInfoFirst = GetAllPatientsInfoByPatientID_Result(patient);

        //    GetAllPatientsInfoByPatientIDJSON(patient);

        //    ViewBag.patientInfo = getPatientInfo;
        //    ViewBag.patientInfoFirst = getPatientInfoFirst;

        //    return View();
        //}

        //public List<GetAllPatientsInfoByPatientID_Result> GetAllPatientsInfoByPatientID(PatientInfoModel patient)
        //{
        //    return db.GetAllPatientsInfoByPatientID(patient.Patient_ID).ToList();
        //}

        //public GetAllPatientsInfoByPatientID_Result GetAllPatientsInfoByPatientID_Result(PatientInfoModel patient)
        //{
        //    return db.GetAllPatientsInfoByPatientID(patient.Patient_ID).FirstOrDefault();
        //}

        //public virtual ActionResult GetAllPatientsInfoByPatientIDJSON(PatientInfoModel patient)
        //{
        //    return Json(db.GetAllPatientsInfoByPatientID(patient.Patient_ID));
        //}

        //public ActionResult Graduation()
        //{
        //    ViewBag.studentsInfo = webDB.Teachers.Find
        //    return View();
        //}
    }
}