using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Blog.Entities;

namespace Blog.DAL
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository() { }

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Member> GetAllMembers()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Member> membersObjectSet = context.CreateObjectSet<Member>();
                return membersObjectSet.ToList();
            }
        }

        public Member GetMember(string email, string password)
        {
            return GetAllMembers().FirstOrDefault(mem => mem.Email == email && mem.Password == password);
        }

        public Member GetMember(string email)
        {
            return GetAllMembers().FirstOrDefault(mem => mem.Email == email);
        }

        public void UpdateMember(Member member)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Member> membersObjectSet = context.CreateObjectSet<Member>();
                Member memberForUpdate = (from mem in membersObjectSet
                                          where mem.MemberId == member.MemberId
                                          select mem).First();

                if (memberForUpdate != null)
                {
                    if (memberForUpdate.Email != member.Email)
                        memberForUpdate.Email = member.Email;

                    if (memberForUpdate.Password != member.Password)
                        memberForUpdate.Password = member.Password;

                    if (memberForUpdate.UserPhoto != member.UserPhoto)
                        memberForUpdate.UserPhoto = member.UserPhoto;

                    if (memberForUpdate.IsAdmin != member.IsAdmin)
                        memberForUpdate.IsAdmin = member.IsAdmin;

                    if (memberForUpdate.IsEnabled != member.IsEnabled)
                        memberForUpdate.IsEnabled = member.IsEnabled;

                    

                    context.SaveChanges();
                }
            }

        }

        public void AddMember(Member member)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var membersObjectSet = context.CreateObjectSet<Member>();

                membersObjectSet.AddObject(member);

                context.SaveChanges();
            }
        }
    }
}
