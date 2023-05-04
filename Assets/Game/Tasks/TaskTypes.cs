namespace Game.Tasks
{
    public enum TaskType
    {
        FindObject = 1,
        Chill = 2,
        Move = 3,
        JumpOn = 4, //прыжок сверху на что-то где гусь в ванной
        PassObject = 5, //прошел мимо объекта => зачислилась ачивка => объект упал
        BreakObject =6,
        CollectAll =7,
        Interract =8
    }
}