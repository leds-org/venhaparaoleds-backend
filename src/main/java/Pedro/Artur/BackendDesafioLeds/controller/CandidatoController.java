package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import Pedro.Artur.BackendDesafioLeds.service.MatchingService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/candidatos")
public class CandidatoController {
    private final MatchingService matchingService;

    public CandidatoController(MatchingService matchingService){
        this.matchingService = matchingService;
    }

    @GetMapping("/{cpf}/concursos")
    public List<ConcursoResponseDTO> buscarConcursosCompativeisPorCpf(@PathVariable String cpf){
        return matchingService.buscarConcursosCompativeisPorCpf(cpf);
    }


}
