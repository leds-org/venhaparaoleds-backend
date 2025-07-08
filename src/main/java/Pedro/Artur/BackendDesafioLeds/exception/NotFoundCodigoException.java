package Pedro.Artur.BackendDesafioLeds.exception;

public class NotFoundCodigoException extends RuntimeException{
    public NotFoundCodigoException(String msg){
        super(msg);
    }
    public NotFoundCodigoException(){
        super("Codigo de concurso não encontrado.");
    }
}
