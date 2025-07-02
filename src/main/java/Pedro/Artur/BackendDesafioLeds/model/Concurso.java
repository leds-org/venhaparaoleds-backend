package Pedro.Artur.BackendDesafioLeds.model;

import jakarta.persistence.*;
import lombok.*;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name="tb_concurso")
public class Concurso {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String orgao;
    private String edital;
    private Long codigo;
    @ElementCollection
    private List<String> profissoes;

}
