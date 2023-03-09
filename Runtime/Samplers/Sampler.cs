namespace Zlitz.Utilities
{
    public interface ISampler2D
    {
        float Sample(float x, float y);
    }

    public interface ISampler3D
    {
        float Sample(float x, float y, float z);
    }
}