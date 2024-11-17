<script>
import { ref } from 'vue';
import axios from 'axios';

export default {
  data() {
    return {
      newUser: {
        nome: '',
        sobrenome: '',
        email: '',
        username: '',
        nascimento: '',
        senha: '',
        confirmacaoSenha: '',
        telefone: '',
        cpf: '',
        foto: null
      },
      address: {
        cep: '',
        rua: '',
        numero: '',
        bairro: '',
        cidade: '',
        estado: ''
      },
      showAddressModal: false // Estado para controlar o modal
    };
  },
  methods: {
    // Função para buscar endereço com base no CEP
    async buscarEndereco() {
      const cep = this.address.cep.replace(/\D/g, '');
      if (cep.length === 8) {
        try {
          const response = await axios.get(`https://viacep.com.br/ws/${cep}/json/`);
          if (response.data && !response.data.erro) {
            const data = response.data;
            this.address.rua = data.logradouro || '';
            this.address.bairro = data.bairro || '';
            this.address.cidade = data.localidade || '';
            this.address.estado = data.uf || '';
          } else {
            alert('CEP não encontrado.');
            this.resetAddressFields();
          }
        } catch (error) {
          console.error('Erro ao buscar o CEP:', error);
          alert('Erro ao buscar o CEP. Tente novamente.');
        }
      }
    },

    // Função para abrir o modal de endereço
    openAddressModal() {
      this.showAddressModal = true;
    },

    // Função para fechar o modal de endereço
    closeAddressModal() {
      this.showAddressModal = false;
    },

    // Função para registrar o usuário
    async submit() {
      if (!this.validateForm()) {
        alert('Preencha todos os campos obrigatórios.');
        return;
      }

      // Validação de senha
      if (this.newUser.senha !== this.newUser.confirmacaoSenha) {
        alert('As senhas não coincidem.');
        return;
      }

      const formData = new FormData();
      Object.keys(this.newUser).forEach(key => {
        if (this.newUser[key]) formData.append(key, this.newUser[key]);
      });
      Object.keys(this.address).forEach(key => {
        formData.append(key, this.address[key]);
      });

      try {
        const response = await axios.post('/api/registrar', formData);
        if (response.status === 201) {
          alert('Registro realizado com sucesso!');
          this.resetForm();
          this.closeAddressModal();

          // Redireciona para a página inicial após o registro
          this.$router.push('/');
        }
      } catch (error) {
        console.error('Erro ao registrar:', error);
        alert('Erro ao registrar. Tente novamente.');
      }
    },

    // Função para validar o formulário antes de enviar
    validateForm() {
      return (
        this.newUser.nome &&
        this.newUser.sobrenome &&
        this.newUser.email &&
        this.address.cep &&
        this.address.rua &&
        this.address.numero &&
        this.address.bairro &&
        this.address.cidade &&
        this.address.estado
      );
    },

    // Função para resetar os campos do formulário
    resetForm() {
      this.newUser = {
        nome: '',
        sobrenome: '',
        email: '',
        username: '',
        nascimento: '',
        senha: '',
        confirmacaoSenha: '',
        telefone: '',
        cpf: '',
        foto: null
      };
      this.resetAddressFields();
    },

    // Função para resetar os campos de endereço
    resetAddressFields() {
      this.address = {
        cep: '',
        rua: '',
        numero: '',
        bairro: '',
        cidade: '',
        estado: ''
      };
    },

    // Função para redirecionar para a página de registro
    redirectToRegister() {
      this.$router.push('/register');
    },

    // Função para redirecionar para a página inicial
    VoltarInicio() {
      this.$router.push('/');
    }
  }
};
</script>



<template>
  <div class="app">
    <!-- Círculos de fundo -->
    <div class="circle small"></div>
    <div class="circle medium"></div>
    <div class="circle extra-large"></div>
    <div class="circle final"></div>
    <div class="circle additional1"></div>
    <div class="circle additional2"></div>
    <div class="circle additional3"></div>

    <!--Div Logo-->
    <div class="logo-gabini"><img src="../assets/images/gabinipreto.png" alt=""></div>


    <!-- Container Principal -->
<div v-if="!showAddressModal" class="container ">
  <button class="buttonVoltar" @click="VoltarInicio">Voltar</button>
  <div class="form-section">
    <h2><span>REGIS</span>TRE-SE</h2>
    

    <!-- Input de Upload de Foto -->
    <div class="profile-upload">
      <label for="upload-photo">
        <img 
          :src="userPhoto ? userPhoto : 'https://via.placeholder.com/150'" 
          alt="Foto de Perfil" 
          class="profile-image" 
        />
        <input 
          type="file" 
          id="upload-photo" 
          @change="handlePhotoUpload" 
          accept="image/*" 
        />
      </label>
    </div>
    <!-- Linha 1: Nome e Sobrenome -->
    <div class="input-row">
      <input type="text" placeholder="Nome" v-model="newUser.nome" required />
      <input type="text" placeholder="Sobrenome" v-model="newUser.sobrenome" required />
    </div>
    
    <!-- Linha 2: Username e Data de Nascimento -->
    <div class="input-row">
      <input type="text" placeholder="Username" v-model="newUser.username" required />
      <input type="date" v-model="newUser.nascimento" required />
    </div>
    
    <!-- Linha 3: Email e Senha -->
    <div class="input-row">
      <input type="email" placeholder="Email" v-model="newUser.email" required />
      <input type="password" placeholder="Senha" v-model="newUser.senha" required />
    </div>

    <!-- Linha 4: Confirmação de Senha e CPF -->
    <div class="input-row">
      <input type="password" placeholder="Confirmação" v-model="newUser.confirmacaoSenha" required />
      <input type="text" placeholder="CPF" v-model="newUser.cpf" required />
    </div>

    <!-- Linha 5: Telefone -->
    <div class="input-group">
      <input type="text" placeholder="Telefone" v-model="newUser.telefone" required />
    </div>
    
    <button @click="openAddressModal" class="button-endereco">Continuar para Endereço</button>
  </div>
</div>


   <!-- Modal de Endereço -->
<div v-if="showAddressModal" class="modal">
 
   
  
  <div class="modal-content">
    <!-- Botão de Voltar abaixo dos inputs -->
   <button @click="showAddressModal = false" class="bottom-button">Voltar</button>

    <h2><span>Ende</span>reço</h2>

    <!-- Linha 1: CEP e Rua -->
    <div class="input-row">
      <input type="text" placeholder="CEP" v-model="address.cep" @blur="buscarEndereco" maxlength="9" required />
      <input type="text" placeholder="Rua" v-model="address.rua" required />
    </div>

    <!-- Linha 2: Número e Bairro -->
    <div class="input-row">
      <input type="text" placeholder="Número" v-model="address.numero" required />
      <input type="text" placeholder="Bairro" v-model="address.bairro" required />
    </div>

    <!-- Linha 3: Cidade e Estado -->
    <div class="input-row">
      <input type="text" placeholder="Cidade" v-model="address.cidade" required />
      <input type="text" placeholder="Estado" v-model="address.estado" required />
    </div>

    <!-- Botão Registrar -->
    <button @click="submit" class="button-endereco">Registrar</button>

   
  </div>
</div>


  </div>
</template>


<style scoped>



body {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  margin: 0;
  background-color: #ffffff;
  overflow: hidden;
}

.circle {
  border-radius: 50%;
  position: absolute;
  opacity: 0.9;
}

.circle.small {
  width: 200px;
  height: 200px;
  margin-bottom: 600px;
  background: #D9D9D9;
  z-index: 3;
}

.circle.medium {
  width: 450px;
  height: 450px;
  background: linear-gradient(207deg, #683636 30.01%, #B8B8B8 68.84%);
  left: 300px;
  margin-top: 150px;
  z-index: 2;
}

.circle.extra-large {
  width: 890px;                                
  height: 890px;
  background: linear-gradient(207deg, #B8B8B8 30.01%, #683636 68.84%);
  left: -320px;
  z-index: 1;
  margin-bottom: 150px
}

.circle.final {
  width: 450px;
  height: 450px;
  background: linear-gradient(194deg, #3A3A3A 46.22%, #5c2323 118.41%);
  right: 50px;
  z-index: 7;
}

.circle.additional1 { width: 300px; height: 300px; top: 65%; right: 20%;  background: #616161; }
.circle.additional2 { width: 400px; height: 400px; top: 20%; left: 50%; background: #634949; }
.circle.additional3 { width: 250px; height: 250px; bottom: 30%; left: 10%; background: #df0c87; }


.logo-gabini {
  position: relative; /* Adicionei esta linha */
  z-index: 100; /* Ajuste para um valor mais alto */
}

.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  background: rgba(58, 58, 58, 0.932);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  border-radius: 20px;
  padding: 40px;
  width: 80%;
  max-width: 900px;
  margin: 40px auto;
  position: relative;
  z-index: 100;
}

.buttonVoltar {
  align-self: flex-start;
  height: 40px;
  width: 100px;
  margin-bottom: 20px;
}

h2 {
  color: #ffffff;
  text-align: center;
  margin-bottom: 30px;

}

.form-section h2{
  font-size: 50px;
  color: black;
}

.form-section span{
  border-bottom: 5px solid black;
}
.form-section {
  width: 100%;
}

.input-row {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  margin-bottom: 20px;
}

/* Estilos para o input de foto de perfil */
.profile-upload {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 30px;
}

.profile-upload label {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  width: 150px;
  height: 150px;
  border-radius: 50%;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  border: 4px solid #ddd;
  background-color: #f5f5f5;
  transition: border-color 0.3s ease;
}

.profile-upload label:hover {
  border-color: #333;
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

.upload-text {
  position: absolute;
  color: #666;
  font-size: 14px;
  text-align: center;
  padding: 0 10px;
}


.input-row input,
.input-group input {
  width: 100%;
  padding: 15px;
  font-size: 16px;
  border-radius: 30px;
  border: 1px solid #b8b8b8;
  background-color: #f5f5f5;
  color: #333;
  outline: none;
  transition: border-color 0.3s ease;
}

input:focus {
  border-color: #5c2323;
}

button {
  padding: 15px;
  background-color: #333;
  color: white;
  border: none;
  border-radius: 50px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease;
  width: 100%;
}

.button-endereco {
  width: 250px;
  margin: 30px auto; /* Centraliza horizontalmente */
  display: block;    /* Garante que o margin funcione corretamente */
  padding: 15px;
  background-color: #333;
  color: white;
  border: none;
  border-radius: 50px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease;
  text-align: center;
}

.button-endereco:hover {
  background-color: #3333339f;
}

.button-endereco:disabled {
  background-color: #aaaaaa;
  cursor: not-allowed;
}

button:hover {
  background-color: #3333339f;
}

button:disabled {
  background-color: #aaaaaa;
  cursor: not-allowed;
}

/* Modal */
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(0, 0, 0, 0.6);
  z-index: 10;
}

.modal-content h2{
  font-size: 50px;
  color: black;
}

.modal-content span{
  border-bottom: 5px solid black;
}

.modal-content {
  background: #3a3a3a;
  padding: 40px;
  border-radius: 20px;
  width: 90%;
  max-width: 600px;
  position: relative;
}

.modal-content h2 {
  color: #070707;
  margin-bottom: 20px;
  text-align: center;
}

.input-row {
  display: flex;
  gap: 20px;
  margin-bottom: 20px;
}

.input-row input {
  flex: 1;
}

.modal-content input {
  padding: 15px;
  font-size: 1rem;
  border: none;
  border-radius: 30px;
  background-color: #ddd;
  outline: none;
  padding-left: 20px;
}

/* Botão Registrar */
.button-endereco {
  background-color: #FFF;
  color: black;
  margin-top: 20px;
  padding: 15px;
  border-radius: 50px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  width: 100%;
}

.button-endereco:hover {
  background-color: #aaaaaa ;
}

.button-endereco:disabled {
  background-color: #aaaaaa;
  cursor: not-allowed;
}

/* Botão de Voltar abaixo dos inputs */
.bottom-button {
  background-color: #FFF;
  color: black;
  margin-top: 20px;
  padding: 10px 15px;
  border-radius: 50px;
  cursor: pointer;
  width: 65px;
  border: none;
  transition: background-color 0.3s ease;
}

.bottom-button:hover {
  background-color: #7e7e7e;
}


</style>

