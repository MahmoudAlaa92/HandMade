//using HandMadeEcommece.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Linq.Dynamic;

//namespace HandMadeEcommece.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GenersController<TEntity,TDto> : ControllerBase where TEntity : class,new()
//    {
//        private readonly AppDbContext Context;
//        private readonly DbSet<TEntity> Source;
//        public GenersController(AppDbContext _Context)
//        {
//            Context = _Context;
//            Source = Context.Set<TEntity>();
//        }

//        [HttpGet]
//        public virtual async Task<IActionResult> GetT(string quary)
//        {
//            if (!string.IsNullOrWhiteSpace(quary)) { return BadRequest(); }
//            var res = await Source.Where(quary).ToListAsync();
//            try
//            {
//                if (res == null) { return NotFound(); }
//                return Ok(res);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }


//        [HttpPost]
//        public virtual async Task<IActionResult>PostT([FromForm]List<TDto> dtos)
//        {
//            if (dtos == null)
//            {
//                return BadRequest();
//            }
//            var list = new List<TEntity>();
//            foreach(var d in dtos)
//            {
//                var data = SetData(d);
//                await Context.AddAsync(data);
//                list.Add(data);
//            }
//                await Context.SaveChangesAsync();

//            return Ok(list);
//        }


//        [HttpPut]
//        public virtual async Task<IActionResult> UpdateT([FromBody]List<ValueTuple<int,TDto>>dtos)
//        {
//            if(dtos == null)
//            {
//                return BadRequest();
//            }
//            var list = new List<TEntity>();
//            foreach(var d in dtos)
//            {
//                var first = d.Item1;
//                if (first <= 0) { continue;  }
//                var entity = await Source.FindAsync(first);
//                if(entity == null) {  continue; }
//                var data = Update(entity,d.Item2);
//                Context.Update(data);
//                list.Add(data);
//            }
//            if(list.Count == 0) {  return BadRequest(); }
//            await Context.SaveChangesAsync();
//            return Ok(list);
//        }


//        [HttpDelete]
//        public virtual async Task<IActionResult> DeleteT(List<int> ids)
//        {
//            if (ids == null) { return BadRequest(); }
//            var list = new List<TEntity>();
//            foreach(var id in ids)
//            {
//                if(id<= 0) { continue; }
//                var entity = await Source.FindAsync(id);
//                if(entity == null) { continue; }
//                list.Add(entity);
//                Context.Remove(entity);
//            }
//            await Context.SaveChangesAsync();
//            return Ok(list);
//        }


//        protected virtual TEntity SetData(TDto dto)
//        {
//            throw new NotImplementedException("SetData must be implemented");
//        } 

//        protected virtual TEntity Update(TEntity entity, TDto dto)
//        {
//            throw new NotImplementedException("Update must be implemented");
//        }
//    }
//}
