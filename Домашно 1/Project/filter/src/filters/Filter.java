package filters;

public interface Filter <T>{
    T execute (T input);
}
