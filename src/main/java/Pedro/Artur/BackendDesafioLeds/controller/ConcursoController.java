package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/concurso")
public class ConcursoController {
    private final ConcursoService concursoService;

    public ConcursoController(ConcursoService concursoService){
        this.concursoService = concursoService;
    }

    @GetMapping
    public List<Concurso> getAll(){
        return concursoService.getAll();

    }
}
