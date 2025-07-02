package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/candidato")
public class CandidatoController {
    private final CandidatoService candidatoService;
    private final ConcursoService concursoService;

    public CandidatoController(CandidatoService candidatoService, ConcursoService concursoService){
        this.candidatoService = candidatoService;
        this.concursoService = concursoService;
    }

    @GetMapping
    public List<Candidato> getAll(){
        return candidatoService.getAll();
    }

    @PostMapping
    public Candidato add(@RequestBody Candidato candidato){
        return candidatoService.save(candidato);
    }

    @GetMapping("/{cpf}/concursos")
    public List<Concurso> BuscarConcursosCompativeisPorCpf(@PathVariable String cpf){
        return concursoService.BuscarConcursosCompativeisPorCpf(cpf);
    }


}
