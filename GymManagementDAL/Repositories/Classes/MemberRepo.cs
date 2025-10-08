using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    internal class MemberRepo : IMemberRepo
    {
        //public GymDBContext dBContext {  get; set; } = new GymDBContext();

        private readonly GymDBContext _dbContext;

        public MemberRepo(GymDBContext _dbContext ) {

            this._dbContext = _dbContext;
                
        }
        public int Add(Member member)
        {
           _dbContext.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var Member = _dbContext.Members.Find( Id);
            if (Member == null)
                return 0;

            _dbContext.Remove(Member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetALL()=>_dbContext.Members.ToList();
       

        public Member? GetById(int id) => _dbContext.Members.Find(id);
        
        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
