namespace Temp.Models.Dtos
{
    public class BaseDto
    {
    }

    public class BaseDto<T> : BaseDto where T : struct
    {
        public T? Id { get; set; }
    }
}
