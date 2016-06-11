using Blog.Entities;
using System.Collections.Generic;

namespace Blog.DAL
{
    public interface IMemberRepository
    {
        List<Member> GetAllMembers();
        Member GetMember(string login,string password);
        Member GetMember(string login);
        void UpdateMember(Member member);
        void AddMember(Member member);
    }
}
