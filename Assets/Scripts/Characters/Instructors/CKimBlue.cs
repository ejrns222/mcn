namespace Characters.Instructors
{
    public class CKimBlue : InstructorBase
    {
        public override long Skill(long calculatedValue)
        {
            throw new System.NotImplementedException();
        }
    
        public CKimBlue()
        {
            Tag = EInstructor.KimBlue;
            Desc = "종합 게임 스트리머지만 주로 총게임을 플레이한다. 돈을 매우 밝힌다. 실력이 상당해서 핵의심을 많이 받지만 이 마저도 영상각으로 볼 정도.";
            Name = "파랑킴";
            SkillName = "깡깡";
            SkillDesc = "물흐르듯 자연스러운 미션 유도로 교육중 마일리지 증가율 + 20%, 깡쥐 보유시 50%";
        }
    }
}
