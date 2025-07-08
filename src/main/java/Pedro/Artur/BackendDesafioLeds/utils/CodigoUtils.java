package Pedro.Artur.BackendDesafioLeds.utils;

public class CodigoUtils {

    public static Boolean validarCodigo(Long codigo){
        return codigo != null && codigo > 0L;
    }
}
