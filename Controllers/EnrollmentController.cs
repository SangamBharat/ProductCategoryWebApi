using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;
using System.Linq.Expressions;

namespace ProductApi.Controllers
{

    [Route("api/[Controller]")]
    public class EnrollmentController : Controller
    {
        private IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Enrollment>>> GetAll()
        {
            try
            {
                return Ok((await _enrollmentService.FindAll()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentByID(Guid id)
        {
            try
            {
                Expression<Func<Enrollment, bool>> expression = e => e.Id == id;
                return Ok((await _enrollmentService.FindByCondition(expression)).First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEnrollmentById([FromRoute] Guid id, [FromBody] Enrollment enrollment)
        {
            Expression<Func<Enrollment, bool>> expression = p => p.Id == id;
            Enrollment? e = (await _enrollmentService.FindByCondition(expression)).FirstOrDefault() ?? null;

            if (e == null)
            {
                return NotFound();
            }

            try
            {
                if (enrollment.Id != id)
                {
                    throw new Exception("Enrollment id is differnt from id provided");
                }
                await _enrollmentService.Update(enrollment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollmentById([FromRoute] Guid id)
        {
            Expression<Func<Enrollment, bool>> expression = e => e.Id == id;
            Enrollment? e = (await _enrollmentService.FindByCondition(expression)).FirstOrDefault() ?? null;

            if (e == null)
            {
                return NotFound();
            }

            try
            {
                await _enrollmentService.Delete(e);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{pId}/{cId}")]
        public async Task<ActionResult> AddEnrollment([FromRoute]int pId,[FromRoute]int cId)
        {
            try
            {
                Guid Id= Guid.NewGuid();
                Enrollment e= new Enrollment();
                e.Id= Id;   
                e.ProductID= pId;   
                e.CategoryID= cId;
                await _enrollmentService.Create(e);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
