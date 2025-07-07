package Pedro.Artur.BackendDesafioLeds.exception;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.method.annotation.MethodArgumentTypeMismatchException;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {

    @ExceptionHandler(NoMatchConcursoException.class)
    private ResponseEntity<String> noMatchConcursoException(NoMatchConcursoException exception){
        return ResponseEntity.status(HttpStatus.NO_CONTENT).body("");
    }

    @ExceptionHandler(NotFoundCodigoException.class)
    private ResponseEntity<String> notFoundCodigoException(NotFoundCodigoException exception){
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Concurso não foi encontrado.");
    }

    @ExceptionHandler(NoMatchCandidatoException.class)
    private ResponseEntity<String> noMatchCandidatoException(NoMatchCandidatoException exception){
        return ResponseEntity.status(HttpStatus.NO_CONTENT).body("");
    }

    @ExceptionHandler(NotFoundCpfException.class)
    private ResponseEntity<String> notFoundCpfException(NotFoundCpfException exception){
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Cpf não foi encontrado.");
    }
    @ExceptionHandler(InvalidParameterException.class)
    private ResponseEntity<String> invalidParameter(InvalidParameterException exception){
        return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Cpf invalido.");
    }

    @ExceptionHandler(MethodArgumentTypeMismatchException.class)
    public ResponseEntity<String> invalidType(MethodArgumentTypeMismatchException exception){
        return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Codigo invalido.");
    }

}
