package Pedro.Artur.BackendDesafioLeds.Utils;

public class CpfUtils {
    public static String limpar(String cpf) {
        return cpf.replaceAll("[^\\d]", "");
    }

    public static String formatar(String cpf) {
        return cpf.replaceAll("(\\d{3})(\\d{3})(\\d{3})(\\d{2})", "$1.$2.$3-$4");
    }
}
