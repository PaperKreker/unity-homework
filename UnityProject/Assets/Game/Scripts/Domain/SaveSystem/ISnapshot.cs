namespace SampleGame.SaveSystem
{
    public interface ISnapshot<T>
    {
        public void Save(T target);
        public void Restore(T target);
    }
}