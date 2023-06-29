using ProductApi.Context;
using ProductApi.Models;
using ProductApi.Services;

namespace EnrollmentApi.Services
{
    public class EnrollmentService : GeneralService<Enrollment>, IEnrollmentService
    {
        public EnrollmentService(ServiceContext serviceContext) : base(serviceContext)
        {

        }
    }
}
