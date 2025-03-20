namespace ProyectoGeneral_01.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository ICategoriaRepository { get; }

        IArticuloRepository IArticuloRepository { get; }

        ISliderRepository ISliderRepository { get; }

        void Save();
    }
}
