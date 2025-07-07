package Pedro.Artur.BackendDesafioLeds.exception;

public class NotFoundCpfException extends RuntimeException{
    public NotFoundCpfException(String msg){
        super(msg);
    }

    public NotFoundCpfException(){
        super("Cpf de candidato não encontrado em nosso sistema!");
    }

}
