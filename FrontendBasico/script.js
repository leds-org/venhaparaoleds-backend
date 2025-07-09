document.addEventListener('DOMContentLoaded', () => {
    // --- Elementos da Interface ---
    const initialScreen = document.getElementById('initialScreen');
    const cadastroScreen = document.getElementById('cadastroScreen');
    const consultaScreen = document.getElementById('consultaScreen');
    const mainTitle = document.getElementById('mainTitle');

    // Botoes de Navegacao Principal
    const btnCadastro = document.getElementById('btnCadastro');
    const btnConsulta = document.getElementById('btnConsulta');
    const backToMainBtn = document.getElementById('backToMain');
    const backToMainFromConsultaBtn = document.getElementById('backToMainFromConsulta');

    // REMOVED: References to showCandidatoFormBtn and showConcursoFormBtn
    const candidatoForm = document.getElementById('candidatoForm');
    const concursoForm = document.getElementById('concursoForm');

    // Elementos do Formulario de Cadastro de Candidato
    const inputCandidatoCPF = document.getElementById('candidatoCPF');
    const inputCandidatoNome = document.getElementById('candidatoNome');
    const inputCandidatoNascimento = document.getElementById('candidatoNascimento');
    const addProfissaoBtn = document.getElementById('addProfissaoBtn');
    const candidatoProfissoesContainer = document.getElementById('candidatoProfissoesContainer');
    const submitCandidatoBtn = document.getElementById('submitCandidatoBtn');
    const feedbackCandidato = document.getElementById('feedbackCandidato');

    // Elementos do Formulario de Cadastro de Concurso
    const inputConcursoOrgao = document.getElementById('concursoOrgao');
    const inputConcursoEdital = document.getElementById('concursoEdital');
    const inputConcursoCodigo = document.getElementById('concursoCodigo');
    const addVagaBtn = document.getElementById('addVagaBtn');
    const concursoVagasContainer = document.getElementById('concursoVagasContainer');
    const submitConcursoBtn = document.getElementById('submitConcursoBtn');
    const feedbackConcurso = document.getElementById('feedbackConcurso');

    // Elementos da Consulta
    const inputCpfConsulta = document.getElementById('inputCpfConsulta');
    const btnBuscarConcursos = document.getElementById('btnBuscarConcursos');
    const resultsConcursos = document.getElementById('resultsConcursos');
    const feedbackConcursosConsulta = document.getElementById('feedbackConcursosConsulta');

    const inputCdConcursoConsulta = document.getElementById('inputCdConcursoConsulta');
    const btnBuscarCandidatos = document.getElementById('btnBuscarCandidatos');
    const resultsCandidatos = document.getElementById('resultsCandidatos');
    const feedbackCandidatosConsulta = document.getElementById('feedbackCandidatosConsulta');


    // --- Funcoes Auxiliares ---
    function showScreen(screenToShow) {
        initialScreen.classList.add('hidden');
        cadastroScreen.classList.add('hidden');
        consultaScreen.classList.add('hidden');
        screenToShow.classList.remove('hidden');
    }

    function showFeedbackMessage(element, message, type) {
        element.textContent = message;
        element.className = `feedback-message show ${type}`;
        setTimeout(() => {
            element.classList.remove('show');
        }, 5000); // Mensagem some apos 5 segundos
    }

    // --- Navegacao ---
    btnCadastro.addEventListener('click', () => {
        showScreen(cadastroScreen);
        // Ao ir para a tela de cadastro, ja exibe ambos os formularios por padrao
        candidatoForm.classList.remove('hidden');
        concursoForm.classList.remove('hidden');
    });

    btnConsulta.addEventListener('click', () => {
        showScreen(consultaScreen);
    });

    backToMainBtn.addEventListener('click', () => {
        showScreen(initialScreen);
    });

    backToMainFromConsultaBtn.addEventListener('click', () => {
        showScreen(initialScreen);
    });

    // --- Gerenciamento de Profissoes para Candidato ---
    addProfissaoBtn.addEventListener('click', () => {
        const newProfissaoGroup = document.createElement('div');
        newProfissaoGroup.classList.add('profissao-input-group');
        newProfissaoGroup.innerHTML = `
            <input type="text" class="candidato-profissao-input" placeholder="Nome da profissao">
            <button class="remove-input-btn">Remover</button>
        `;
        candidatoProfissoesContainer.appendChild(newProfissaoGroup);

        // Adiciona evento de clique para o novo botao de remover
        newProfissaoGroup.querySelector('.remove-input-btn').addEventListener('click', (event) => {
            event.target.closest('.profissao-input-group').remove();
        });
    });

    // --- Gerenciamento de Vagas/Profissoes para Concurso ---
    addVagaBtn.addEventListener('click', () => {
        const newVagaProfissaoGroup = document.createElement('div');
        newVagaProfissaoGroup.classList.add('vaga-input-group');
        newVagaProfissaoGroup.innerHTML = `
            <input type="text" class="concurso-vaga-profissao-input" placeholder="Profissao necessaria para a vaga">
            <button class="remove-input-btn">Remover</button>
        `;
        concursoVagasContainer.appendChild(newVagaProfissaoGroup);

        // Adiciona evento de clique para o novo botao de remover
        newVagaProfissaoGroup.querySelector('.remove-input-btn').addEventListener('click', (event) => {
            event.target.closest('.vaga-input-group').remove();
        });
    });

    // --- Cadastro de Candidato ---
    submitCandidatoBtn.addEventListener('click', async () => {
        const cpf = inputCandidatoCPF.value.trim();
        const nome = inputCandidatoNome.value.trim();
        const nascimento = inputCandidatoNascimento.value; // Formato YYYY-MM-DD
        const profissoesInputs = candidatoProfissoesContainer.querySelectorAll('.candidato-profissao-input');
        const profissoes = Array.from(profissoesInputs)
            .map(input => ({ NomeProf: input.value.trim() }))
            .filter(p => p.NomeProf !== ''); // Filtra profissoes vazias

        if (!cpf || !nome || !nascimento || profissoes.length === 0) {
            showFeedbackMessage(feedbackCandidato, 'Por favor, preencha todos os campos e adicione ao menos uma profissao para o candidato.', 'error');
            return;
        }

        const candidatoData = {
            CPF: cpf,
            Nome: nome,
            Nascimento: nascimento,
            Profissoes: profissoes
        };

        console.log('Dados do Candidato a serem enviados:', candidatoData);

        try {
            const response = await fetch('https://localhost:7285/api/Cadastro/candidato', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(candidatoData)
            });

            if (response.ok) {
                showFeedbackMessage(feedbackCandidato, 'Candidato cadastrado com sucesso!', 'success');
                // Limpa o formulario
                inputCandidatoCPF.value = '';
                inputCandidatoNome.value = '';
                inputCandidatoNascimento.value = '';
                candidatoProfissoesContainer.innerHTML = `
                    <label>Profissoes:</label>
                    <div class="profissao-input-group">
                        <input type="text" class="candidato-profissao-input" placeholder="Nome da profissao">
                        <button class="remove-input-btn">Remover</button>
                    </div>
                `;
                // Re-adiciona evento de clique para o botao de remover padrao
                candidatoProfissoesContainer.querySelector('.remove-input-btn').addEventListener('click', (event) => {
                    event.target.closest('.profissao-input-group').remove();
                });
            } else {
                const errorData = await response.json();
                showFeedbackMessage(feedbackCandidato, `Erro ao cadastrar candidato: ${errorData.title || errorData.detail || JSON.stringify(errorData)}`, 'error');
            }
        } catch (error) {
            console.error('Erro na requisicao de cadastro de candidato:', error);
            showFeedbackMessage(feedbackCandidato, 'Erro de conexao ao cadastrar candidato. Verifique o console.', 'error');
        }
    });

    // --- Cadastro de Concurso ---
    submitConcursoBtn.addEventListener('click', async () => {
        const orgao = inputConcursoOrgao.value.trim();
        const edital = inputConcursoEdital.value.trim();
        const codigo = inputConcursoCodigo.value.trim();
        const profissoesVagaInputs = concursoVagasContainer.querySelectorAll('.concurso-vaga-profissao-input');
        const ProfissoesNecessarias = Array.from(profissoesVagaInputs)
            .map(input => ({ NomeProf: input.value.trim() }))
            .filter(p => p.NomeProf !== ''); // Filtra profissoes vazias

        if (!orgao || !edital || !codigo || ProfissoesNecessarias.length === 0) {
            showFeedbackMessage(feedbackConcurso, 'Por favor, preencha todos os campos e adicione ao menos uma profissao necessaria para a vaga do concurso.', 'error');
            return;
        }

        const concursoData = {
            Orgao: orgao,
            Edital: edital,
            CdConcurso: parseInt(codigo),
            Vagas: [{
                NomeVag: "Vaga Padrao", // Nome da vaga padrao, ja que nao ha campo para isso no front atual
                ProfissoesNecessarias: ProfissoesNecessarias
            }]
        };

        console.log('Dados do Concurso a serem enviados:', concursoData);

        try {
            const response = await fetch('https://localhost:7285/api/Cadastro/concurso', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(concursoData)
            });

            if (response.ok) {
                showFeedbackMessage(feedbackConcurso, 'Concurso cadastrado com sucesso!', 'success');
                // Limpa o formulario
                inputConcursoOrgao.value = '';
                inputConcursoEdital.value = '';
                inputConcursoCodigo.value = '';
                concursoVagasContainer.innerHTML = `
                    <label>Profissoes Requeridas para Vagas:</label>
                    <div class="vaga-input-group">
                        <input type="text" class="concurso-vaga-profissao-input" placeholder="Profissao necessaria para a vaga">
                        <button class="remove-input-btn">Remover</button>
                    </div>
                `;
                // Re-adiciona evento de clique para o botao de remover padrao
                concursoVagasContainer.querySelector('.remove-input-btn').addEventListener('click', (event) => {
                    event.target.closest('.vaga-input-group').remove();
                });

            } else {
                const errorData = await response.json();
                showFeedbackMessage(`Erro ao cadastrar concurso: ${errorData.title || errorData.detail || JSON.stringify(errorData)}`, 'error');
            }
        } catch (error) {
            console.error('Erro na requisicao de cadastro de concurso:', error);
            showFeedbackMessage(feedbackConcurso, 'Erro de conexao ao cadastrar concurso. Verifique o console.', 'error');
        }
    });

    // --- Consultas ---
    // Buscar Concursos por CPF
    btnBuscarConcursos.addEventListener('click', async () => {
        const cpf = inputCpfConsulta.value.trim();
        if (!cpf) {
            showFeedbackMessage(feedbackConcursosConsulta, 'Por favor, digite o CPF do candidato.', 'error');
            resultsConcursos.textContent = '';
            return;
        }

        try {
            const response = await fetch(`https://localhost:7285/api/Importacao/concursos-por-cpf?cpf=${cpf}`);
            if (response.ok) {
                const responseData = await response.json(); // Renomeado para evitar conflito com 'data'
                // Verifica se a resposta est� encapsulada em '$values'
                const concursos = responseData && responseData.$values ? responseData.$values : responseData;

                resultsConcursos.innerHTML = ''; // Limpa resultados anteriores

                if (concursos.length === 0) {
                    resultsConcursos.innerHTML = `<p>Nenhum concurso encontrado para o CPF <strong>${cpf}</strong>.</p>`;
                    showFeedbackMessage(feedbackConcursosConsulta, 'Nenhum concurso encontrado.', 'info');
                } else {
                    concursos.forEach(concurso => { // Agora 'concursos' � garantido ser um array
                        const p = document.createElement('p');
                        p.innerHTML = `
                            <strong>�rg�o:</strong> ${concurso.orgao}<br>
                            <strong>C�digo:</strong> ${concurso.cdConcurso}<br>
                            <strong>Edital:</strong> ${concurso.edital}
                            <hr> `;
                        resultsConcursos.appendChild(p);
                    });
                    showFeedbackMessage(feedbackConcursosConsulta, 'Concursos encontrados com sucesso!', 'success');
                }
            } else if (response.status === 404) {
                resultsConcursos.textContent = `Nenhum concurso encontrado para o CPF: ${cpf}.`;
                showFeedbackMessage(feedbackConcursosConsulta, 'Nenhum concurso encontrado.', 'info');
            }
            else {
                const errorData = await response.text();
                resultsConcursos.textContent = `Erro: ${errorData || response.statusText}`;
                showFeedbackMessage(feedbackConcursosConsulta, `Erro ao buscar concursos: ${errorData || response.statusText}`, 'error');
                console.error('Erro de API ao buscar concursos por CPF:', response.status, errorData);
            }
        } catch (error) {
            console.error('Erro na requisicao de buscar concursos:', error);
            showFeedbackMessage(feedbackConcursosConsulta, 'Erro de conexao ao buscar concursos. Verifique o console.', 'error');
            resultsConcursos.textContent = 'Erro de conexao. Verifique o console.';
        }
    });

    // Buscar Candidatos por Codigo do Concurso
    btnBuscarCandidatos.addEventListener('click', async () => {
        const cdConcurso = inputCdConcursoConsulta.value.trim();
        if (!cdConcurso) {
            showFeedbackMessage(feedbackCandidatosConsulta, 'Por favor, digite o codigo do concurso.', 'error');
            resultsCandidatos.textContent = '';
            return;
        }

        try {
            const response = await fetch(`https://localhost:7285/api/Importacao/candidatos-por-concurso?cdConcurso=${cdConcurso}`);

                                                            
            if (response.ok) {
                const responseData = await response.json(); // Renomeado para evitar conflito com 'data'
                // Verifica se a resposta est� encapsulada em '$values'
                const candidatos = responseData && responseData.$values ? responseData.$values : responseData;


                resultsCandidatos.innerHTML = ''; // Limpa resultados anteriores

                if (candidatos.length === 0) {
                    resultsCandidatos.innerHTML = `<p>Nenhum candidato encontrado para o concurso com c�digo <strong>${cdConcurso}</strong>.</p>`;
                    showFeedbackMessage(feedbackCandidatosConsulta, 'Nenhum candidato encontrado.', 'info');
                } else {
                    candidatos.forEach(candidato => { // Agora 'candidatos' � garantido ser um array
                        // Assumindo que 'nascimento' � um campo de data em um formato que pode ser parseado pelo Date (ex: ISO string)
                        const dataNasc = new Date(candidato.nascimento).toLocaleDateString('pt-BR');
                        const p = document.createElement('p');
                        p.innerHTML = `
                            <strong>Nome:</strong> ${candidato.nome}<br>
                            <strong>Nascimento:</strong> ${dataNasc}<br>
                            <strong>CPF:</strong> ${candidato.cpf}
                            <hr> `;
                        resultsCandidatos.appendChild(p);
                    });
                    showFeedbackMessage(feedbackCandidatosConsulta, 'Candidatos encontrados com sucesso!', 'success');
                }
            } else if (response.status === 404) {
                resultsCandidatos.textContent = `Nenhum candidato encontrado para o concurso com codigo: ${cdConcurso}.`;
                showFeedbackMessage(feedbackCandidatosConsulta, 'Nenhum candidato encontrado.', 'info');
            }
            else {
                const errorData = await response.text();
                resultsCandidatos.textContent = `Erro: ${errorData || response.statusText}`;
                showFeedbackMessage(feedbackCandidatosConsulta, `Erro ao buscar candidatos: ${errorData || response.statusText}`, 'error');
                console.error('Erro de API ao buscar candidatos por concurso:', response.status, errorData);
            }
        } catch (error) {
            console.error('Erro na requisicao de buscar candidatos:', error);
            showFeedbackMessage(feedbackCandidatosConsulta, 'Erro de conexao ao buscar candidatos. Verifique o console.', 'error');
            resultsCandidatos.textContent = 'Erro de conexao. Verifique o console.';
        }
    });

    // Formatacao do CPF
    inputCandidatoCPF.addEventListener('input', (e) => {
        let value = e.target.value.replace(/\D/g, ''); // Remove tudo que nao e digito
        if (value.length > 0) {
            value = value.replace(/(\d{3})(\d)/, '$1.$2');
            value = value.replace(/(\d{3})(\d)/, '$1.$2');
            value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        }
        e.target.value = value;
    });

    inputCpfConsulta.addEventListener('input', (e) => {
        let value = e.target.value.replace(/\D/g, ''); // Remove tudo que nao e digito
        if (value.length > 0) {
            value = value.replace(/(\d{3})(\d)/, '$1.$2');
            value = value.replace(/(\d{3})(\d)/, '$1.$2');
            value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        }
        e.target.value = value;
    });


    // Adiciona evento de clique para os botoes de remover que ja existem na carga inicial
    document.querySelectorAll('.profissao-input-group .remove-input-btn, .vaga-input-group .remove-input-btn').forEach(button => {
        button.addEventListener('click', (event) => {
            event.target.closest('.profissao-input-group, .vaga-input-group').remove();
        });
    });

    // Iniciar na tela inicial
    showScreen(initialScreen);
});