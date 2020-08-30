namespace Characters.Instructors
{
    public class CDaedo : InstructorBase
    {
        public override long Skill(long calculatedValue)
        {
            throw new System.NotImplementedException();
        }

        public CDaedo()
        {
            Tag = EInstructor.Daedo;
            Desc = "1인 미디어계의 선구자. 삼촌독서실의 사장님이다.";
            Name = "대독서실";
            SkillName = "선구자";
            SkillDesc = "수많은 1인방송인들이 이 사람을 보고 꿈을 키웠다. \n교육중 획득 마일리지 증가율이 교육중인 스트리머수 X 10% 만큼 증가";
        }
    }
}
