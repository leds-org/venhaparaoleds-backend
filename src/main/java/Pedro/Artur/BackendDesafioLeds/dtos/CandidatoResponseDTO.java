package Pedro.Artur.BackendDesafioLeds.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CandidatoResponseDTO{
    private String nome;
    private String dataNascimento;
    private String cpf;

}
