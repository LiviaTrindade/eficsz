<script>
export default {
  data() {
    return {
      user: {},
      isEditing: false,
      novoEndereco: {
        cep: '',
        rua: '',
        numero: '',
        bairro: '',
        cidade: '',
        estado: ''
      }
    };
  },
  created() {
    const savedUser = JSON.parse(localStorage.getItem('usuarioRegistrado'));
    if (savedUser) {
      this.user = savedUser;
      if (!this.user.enderecos) {
        this.user.enderecos = [];
      }
    } else {
      alert('Nenhum usuário registrado.');
      this.$router.push('/register');
    }
  },
  methods: {
    voltarInicio() {
      this.$router.push('/');
    },
    editarPerfil() {
      this.isEditing = true;
    },
    cancelarEdicao() {
      this.isEditing = false;
    },
    salvarEdicao() {
      localStorage.setItem('usuarioRegistrado', JSON.stringify(this.user));
      alert('Perfil atualizado com sucesso!');
      this.isEditing = false;
    },
    buscarEndereco() {
      const cep = this.novoEndereco.cep.replace(/\D/g, '');
      if (cep.length === 8) {
        fetch(`https://viacep.com.br/ws/${cep}/json/`)
          .then(response => response.json())
          .then(data => {
            if (!data.erro) {
              this.novoEndereco.rua = data.logradouro || '';
              this.novoEndereco.bairro = data.bairro || '';
              this.novoEndereco.cidade = data.localidade || '';
              this.novoEndereco.estado = data.uf || '';
            }
          });
      }
    },
    adicionarEndereco() {
      if (this.novoEndereco.cep && this.novoEndereco.rua && this.novoEndereco.numero) {
        this.user.enderecos.push({ ...this.novoEndereco });
        this.novoEndereco = { cep: '', rua: '', numero: '', bairro: '', cidade: '', estado: '' };
        localStorage.setItem('usuarioRegistrado', JSON.stringify(this.user));
        alert('Endereço adicionado!');
      }
    }
  }
};
</script>

<template>
    <div class="app">
      <!-- Container Principal -->
      <div class="container">
        <!-- Botão para voltar -->
        <button class="buttonVoltar" @click="voltarInicio">Voltar</button>
  
        <!-- Seção de Foto e Informações do Usuário -->
        <div class="profile-header">
          <div class="profile-upload">
            <label>
              <img :src="user.foto || 'https://via.placeholder.com/150'" alt="Foto de Perfil" class="profile-image" />
              <input type="file" @change="uploadFoto" />
            </label>
          </div>
          <h2>{{ user.nome }} {{ user.sobrenome }}</h2>
          <p class="username">@{{ user.username }}</p>
        </div>
  
        <!-- Seção de Informações e Edição -->
        <div v-if="!isEditing" class="info-section">
          <h3>Informações Pessoais</h3>
          <p><strong>Email:</strong> {{ user.email }}</p>
          <p><strong>Data de Nascimento:</strong> {{ user.nascimento }}</p>
          <p><strong>Telefone:</strong> {{ user.telefone }}</p>
          <p><strong>CPF:</strong> {{ user.cpf }}</p>
  
          <h3>Endereços</h3>
          <div v-for="(endereco, index) in user.enderecos" :key="index" class="address-card">
            <p><strong>CEP:</strong> {{ endereco.cep }}</p>
            <p><strong>Rua:</strong> {{ endereco.rua }}</p>
            <p><strong>Número:</strong> {{ endereco.numero }}</p>
            <p><strong>Bairro:</strong> {{ endereco.bairro }}</p>
            <p><strong>Cidade:</strong> {{ endereco.cidade }}</p>
            <p><strong>Estado:</strong> {{ endereco.estado }}</p>
          </div>
          <button class="button-endereco" @click="editarPerfil">Editar Perfil</button>
        </div>
  
        <!-- Formulário de Edição -->
        <div v-else class="edit-section">
          <form @submit.prevent="salvarEdicao">
            <input type="text" v-model="user.nome" placeholder="Nome" required />
            <input type="text" v-model="user.sobrenome" placeholder="Sobrenome" required />
            <input type="email" v-model="user.email" placeholder="Email" required />
            <input type="date" v-model="user.nascimento" required />
            <input type="text" v-model="user.telefone" placeholder="Telefone" required />
            <input type="text" v-model="user.cpf" placeholder="CPF" required />
  
            <h3>Adicionar Endereço</h3>
            <div class="input-row">
              <input type="text" v-model="novoEndereco.cep" placeholder="CEP" required />
              <input type="text" v-model="novoEndereco.rua" placeholder="Rua" required />
            </div>
            <div class="input-row">
              <input type="text" v-model="novoEndereco.numero" placeholder="Número" required />
              <input type="text" v-model="novoEndereco.bairro" placeholder="Bairro" required />
            </div>
            <div class="input-row">
              <input type="text" v-model="novoEndereco.cidade" placeholder="Cidade" required />
              <input type="text" v-model="novoEndereco.estado" placeholder="Estado" required />
            </div>
            <button type="button" @click="adicionarEndereco">Adicionar Endereço</button>
            <button type="submit">Salvar</button>
            <button type="button" @click="cancelarEdicao">Cancelar</button>
          </form>
        </div>
      </div>
    </div>
  </template>
  
<style scoped>
  body, html {
    margin: 0;
    padding: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: #f0f0f0;
  }
  
  .app {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
  }
  
  .container {
    background: rgba(58, 58, 58, 0.932);
    padding: 40px;
    border-radius: 20px;
    box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 90%;
    max-width: 600px;
  }
  
  .profile-header {
    text-align: center;
    margin-bottom: 30px;
  }
  
  .profile-upload {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 20px;
  }
  
  .profile-upload label {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    width: 150px;
    height: 150px;
    border-radius: 50%;
    overflow: hidden;
    cursor: pointer;
    border: 4px solid #ddd;
    background-color: #f5f5f5;
    transition: border-color 0.3s ease;
  }
  
  .profile-upload input[type="file"] {
    display: none;
  }
  
  .profile-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
    border-radius: 50%;
  }
  
  h2, h3 {
    color: #fff;
  }
  
  .info-section p {
    color: #ddd;
  }
  
  button {
    margin-top: 15px;
    padding: 15px;
    background-color: #333;
    color: white;
    border: none;
    border-radius: 50px;
    cursor: pointer;
    width: 100%;
    transition: background-color 0.3s ease;
  }
  
  button:hover {
    background-color: #555;
  }
  
  .input-row {
    display: flex;
    justify-content: space-between;
    gap: 10px;
    margin-bottom: 20px;
  }
  
  input {
    width: 100%;
    padding: 10px;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 8px;
    background-color: #f5f5f5;
    color: #333;
  }
  
  input:focus {
    border-color: #5c2323;
    outline: none;
  }
</style>  