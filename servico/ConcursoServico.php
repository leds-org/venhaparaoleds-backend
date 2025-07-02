<?php

class ConcursoServico
{
    private $candidatos = [];
    private $concursos = [];

    public function __construct($arquivoCandidatos, $arquivoConcursos)
    {
        $this->candidatos = $this->carregarArquivo($arquivoCandidatos);
        $this->concursos = $this->carregarArquivo($arquivoConcursos);
    }

    private function carregarArquivo($arquivo)
    {
        $linhas = file($arquivo, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
        array_shift($linhas); // remove o cabeçalho

        $dados = [];

        foreach ($linhas as $linha) {
            $partes = explode("\t", $linha);

            if (count($partes) < 4) continue;

            $campo4 = json_decode($partes[3], true);
            if (!is_array($campo4)) $campo4 = [];

            $dados[] = [
                'campo1' => trim($partes[0]),
                'campo2' => trim($partes[1]),
                'campo3' => trim($partes[2]),
                'campo4' => array_map('trim', $campo4),
            ];
        }

        return $dados;
    }

    // Retorna concursos compatíveis com o CPF informado
    public function buscarConcursosPorCpf($cpf)
    {
        $resultado = [];

        foreach ($this->candidatos as $cand) {
            if ($cand['campo3'] === $cpf) {
                $profissoes = $cand['campo4'];

                foreach ($this->concursos as $conc) {
                    if (array_intersect($profissoes, $conc['campo4'])) {
                        $resultado[] = [
                            'orgao' => $conc['campo1'],
                            'edital' => $conc['campo2'],
                            'codigo' => $conc['campo3'],
                        ];
                    }
                }
            }
        }

        return $resultado;
    }

    // Retorna candidatos compatíveis com o código do concurso
    public function buscarCandidatosPorCodigo($codigo)
    {
        $vagas = [];

        foreach ($this->concursos as $conc) {
            if ($conc['campo3'] === $codigo) {
                $vagas = array_merge($vagas, $conc['campo4']);
            }
        }

        $vagas = array_unique($vagas);
        $resultado = [];

        foreach ($this->candidatos as $cand) {
            if (array_intersect($cand['campo4'], $vagas)) {
                $resultado[] = [
                    'nome' => $cand['campo1'],
                    'nascimento' => $cand['campo2'],
                    'cpf' => $cand['campo3'],
                ];
            }
        }

        return $resultado;
    }
}
