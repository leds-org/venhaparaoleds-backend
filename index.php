<?php
require_once 'servico/ConcursoServico.php';

$servico = new ConcursoServico('data/candidatos.txt', 'data/concursos.txt');

// Inicializa as variáveis para evitar warnings
$cpf = $_POST['cpf'] ?? '';
$codigo = $_POST['codigo'] ?? '';

$concursos = [];
$candidatos = [];

if ($cpf !== '') {
    $concursos = $servico->buscarConcursosPorCpf($cpf);
}

if ($codigo !== '') {
    $candidatos = $servico->buscarCandidatosPorCodigo($codigo);
}
?>

<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8" />
    <title>Desafio Backend - Consulta Concursos</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #e8f5e9; /* verde claro suave */
            color: #1b5e20; /* verde escuro */
        }
        h1, h2 {
            color: #2e7d32; /* verde médio */
        }
        form {
            margin-bottom: 20px;
            background: #a5d6a7; /* verde médio claro */
            padding: 15px 20px;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(46, 125, 50, 0.3);
            max-width: 600px;
        }
        label {
            font-weight: bold;
            display: block;
            margin-bottom: 8px;
        }
        input[type="text"] {
            padding: 8px;
            width: 100%;
            max-width: 100%;
            border: 2px solid #388e3c;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 1rem;
            color: #1b5e20;
        }
        input[type="text"]:focus {
            outline: none;
            border-color: #1b5e20;
            background-color: #c8e6c9;
        }
        button {
            margin-top: 10px;
            padding: 10px 18px;
            background-color: #2e7d32;
            border: none;
            border-radius: 5px;
            color: white;
            font-weight: bold;
            cursor: pointer;
            font-size: 1rem;
            transition: background-color 0.3s ease;
        }
        button:hover {
            background-color: #1b5e20;
        }
        table {
            border-collapse: collapse;
            width: 100%;
            max-width: 600px;
            background: white;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 3px 7px rgba(0, 0, 0, 0.1);
        }
        th, td {
            border: 1px solid #a5d6a7;
            padding: 10px 15px;
            text-align: left;
            color: #1b5e20;
        }
        th {
            background-color: #81c784;
            font-weight: 600;
        }
        p {
            max-width: 600px;
            font-style: italic;
        }
        hr {
            margin: 40px 0;
            border: none;
            border-top: 2px solid #a5d6a7;
            max-width: 600px;
        }
    </style>
</head>
<body>
    <h1>Consulta de Concursos e Candidatos</h1>

    <form method="post">
        <label for="cpf">Buscar Concursos por CPF:</label>
        <input type="text" name="cpf" id="cpf" placeholder="Digite o CPF (ex: 565.512.353-92)" value="<?= htmlspecialchars($cpf) ?>" />
        <button type="submit">Buscar</button>
    </form>

    <?php if ($cpf): ?>
        <h2>Concursos compatíveis com CPF <?= htmlspecialchars($cpf) ?>:</h2>
        <?php if (count($concursos) > 0): ?>
            <table>
                <thead>
                    <tr>
                        <th>Órgão</th>
                        <th>Código</th>
                        <th>Edital</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($concursos as $c): ?>
                        <tr>
                            <td><?= htmlspecialchars($c['orgao']) ?></td>
                            <td><?= htmlspecialchars($c['codigo']) ?></td>
                            <td><?= htmlspecialchars($c['edital']) ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        <?php else: ?>
            <p>Nenhum concurso encontrado para esse CPF.</p>
        <?php endif; ?>
    <?php endif; ?>

    <hr />

    <form method="post">
        <label for="codigo">Buscar Candidatos por Código do Concurso:</label>
        <input type="text" name="codigo" id="codigo" placeholder="Digite o código do concurso" value="<?= htmlspecialchars($codigo) ?>" />
        <button type="submit">Buscar</button>
    </form>

    <?php if ($codigo): ?>
        <h2>Candidatos compatíveis com o concurso <?= htmlspecialchars($codigo) ?>:</h2>
        <?php if (count($candidatos) > 0): ?>
            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Data de Nascimento</th>
                        <th>CPF</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($candidatos as $c): ?>
                        <tr>
                            <td><?= htmlspecialchars($c['nome']) ?></td>
                            <td><?= htmlspecialchars($c['nascimento']) ?></td>
                            <td><?= htmlspecialchars($c['cpf']) ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        <?php else: ?>
            <p>Nenhum candidato encontrado para esse código de concurso.</p>
        <?php endif; ?>
    <?php endif; ?>
</body>
</html>
