package Pedro.Artur.BackendDesafioLeds.exception;

public class NoMatchConcursoException extends RuntimeException {
    public NoMatchConcursoException() {
        super("Nenhum Concurso compatível com este candidato");
    }

    public NoMatchConcursoException(String cpf) {
        super("Nenhum Concurso compatível com este candidato, cpf:"+cpf);
    }
}
