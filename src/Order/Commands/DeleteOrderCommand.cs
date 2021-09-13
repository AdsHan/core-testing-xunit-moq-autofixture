namespace Order.Commands
{

    public class DeleteOrderCommand : Command
    {
        public DeleteOrderCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}