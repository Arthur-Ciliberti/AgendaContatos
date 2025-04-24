$(document).ready(function () {
    $('#cpf').mask('000.000.000-00');
    $('#telefone').mask('(00) 00000-0000');
});

const apiBase = 'http://localhost:5195/api/contatos';

function cadastrarContato() {
    const nome = $('#nome').val().trim();
    const apelido = $('#apelido').val().trim();
    const cpf = $('#cpf').val().replace(/\D/g, '');
    const telefone = $('#telefone').val().trim();
    const email = $('#email').val().trim();

    if (!nome || !apelido || !cpf || !telefone || !email) {
        alert('Preencha todos os campos antes de cadastrar.');
        return;
    }

    const contato = { nome, apelido, cpf, telefone, email };

    $.ajax({
        url: apiBase,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(contato),
        success: () => {
            alert('Contato cadastrado!');
            listarTodos();
            limparFormulario();
        },
        error: () => alert('Erro ao cadastrar')
    });
}

function listarTodos() {
    $('#filtroId').val('');
    $.get(apiBase, function (data) {
        preencherTabela(data);
    });
}

function listarUltimos10Dias() {
    $('#filtroId').val('');
    const dataLimite = new Date();
    dataLimite.setDate(dataLimite.getDate() - 10);
    $.get(apiBase, function (data) {
        const filtrados = data.filter(c => new Date(c.dataCadastro) >= dataLimite);
        preencherTabela(filtrados);
    });
}

function buscarPorId() {
    const id = $('#filtroId').val();
    if (!id) return;
    $.get(`${apiBase}/${id}`, function (data) {
        preencherTabela([data]);
    }).fail(() => alert('Contato não encontrado'));
}

function preencherTabela(contatos) {
    const tbody = $('#tabelaContatos tbody');
    tbody.empty();
    contatos.forEach(c => {
        tbody.append(`
                  <tr>
                    <td>${c.id}</td>
                    <td><input value="${c.nome}" id="nome-${c.id}"></td>
                    <td><input value="${c.apelido}" id="apelido-${c.id}"></td>
                    <td><input value="${c.cpf}" id="cpf-${c.id}"></td>
                    <td><input value="${c.telefone}" id="telefone-${c.id}"></td>
                    <td><input value="${c.email}" id="email-${c.id}"></td>
                    <td>
                      <button onclick="atualizar(${c.id})">Alterar</button>
                      <button onclick="excluir(${c.id})">Excluir</button>
                    </td>
                  </tr>
                `);
    });
}

function atualizar(id) {
    const contato = {
        nome: $(`#nome-${id}`).val(),
        apelido: $(`#apelido-${id}`).val(),
        cpf: $(`#cpf-${id}`).val().replace(/\D/g, ''),
        telefone: $(`#telefone-${id}`).val(),
        email: $(`#email-${id}`).val()
    };
    $.ajax({
        url: `${apiBase}/${id}`,
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(contato),
        success: () => {
            alert('Atualizado com sucesso');
            listarTodos();
        },
        error: () => alert('Erro ao atualizar')
    });
}

function excluir(id) {
    if (!confirm('Tem certeza que deseja excluir?')) return;
    $.ajax({
        url: `${apiBase}/${id}`,
        method: 'DELETE',
        success: () => {
            alert('Excluído com sucesso');
            listarTodos();
        },
        error: () => alert('Erro ao excluir')
    });
}

function limparFormulario() {
    $('#nome, #apelido, #cpf, #telefone, #email').val('');
}

function mostrarFormulario() {
    $('#formularioContato').show();
    $('#btnMostrarFormulario').hide();
}

function cancelarCadastro() {
    $('#formularioContato').hide();
    $('#btnMostrarFormulario').show();
    limparFormulario();
}

function toggleContatos() {
    const area = $('#areaContatos');
    if (area.is(':visible')) {
        area.slideUp();
    } else {
        listarTodos();
        area.slideDown();
    }
}

function toggleBackups() {
    const area = $('#areaBackups');
    if (area.is(':visible')) {
        area.slideUp();
    } else {
        listarBackups();
        area.slideDown();
    }
}

function listarBackups() {
    $.get(`${apiBase}/backup`, function (data) {
        const tbody = $('#tabelaBackups tbody');
        tbody.empty();

        data.forEach(b => {
            tbody.append(`
        <tr>
          <td>${b.id}</td>
          <td>${b.nome}</td>
          <td>${b.apelido}</td>
          <td>${b.cpf}</td>
          <td>${b.telefone}</td>
          <td>${b.email}</td>
        </tr>
      `);
        });
    });
}