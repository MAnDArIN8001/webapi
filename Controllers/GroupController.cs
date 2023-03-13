using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> getGroup() {
            return Ok(await _groupService.getGroup());
        }

        [HttpGet("studetn/{id}")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> getStudent(int id) {
            return Ok(await _groupService.GetStudent(id));
        }

        [HttpPost("student/add")] 
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> addStudent(AddStudentDto newStudent) {
            return Ok(await _groupService.addStudent(newStudent));
        }

        [HttpPut("student/set")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> setStudent(UpdateStudentDto updatedStudent) {
            var response = await _groupService.updateStudent(updatedStudent);

            if(!response.Success){
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("student/delete")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> deleteStudent(int id) {
            var response = await _groupService.deleteStudent(id);

            if(!response.Success){
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}