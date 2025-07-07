package Pedro.Artur.BackendDesafioLeds.exception;

public class NoMatchCandidatoException extends RuntimeException {
    public NoMatchCandidatoException() {
        super("Nenhum Candidato compativel com este concurso");
    }
    public NoMatchCandidatoException(String message) {
        super(message);
    }
}
