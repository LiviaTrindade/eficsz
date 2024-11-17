<script>
import axios from "axios";
import { ref } from 'vue';

export default {
  data() {
    return {
      username: "",
      password: "",
      remember: false,
    };
  },
  methods: {
    // Função para lidar com o envio do formulário de login
    async handleLogin() {
      try {
        // Substituir o endpoint pela rota correta da API
        const response = await axios.post('http://localhost:5000/api/login', {
          username: this.username,
          password: this.password,
        });

        // Verificação da resposta do servidor
        if (response.status === 200) {
          alert("Login realizado com sucesso!");

          // Salvar token no localStorage se 'Lembrar' estiver marcado
          if (this.remember && response.data.token) {
            localStorage.setItem("authToken", response.data.token);
          }

          // Redirecionar para o dashboard após login bem-sucedido
          this.$router.push("/dashboard");
        } else {
          alert("Credenciais inválidas.");
        }
      } catch (error) {
        console.error("Erro ao fazer login:", error);
        alert("Erro ao fazer login. Tente novamente.");
      }
    },
    // Função para redirecionar para a página de registro
    redirectToRegister() {
      this.$router.push("/registration");
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

    
    <!-- Container principal -->
    <div class="container">
      <!-- Seção esquerda -->
      <div class="left-section">
        <h1>BEM VINDO</h1>
        <button @click="redirectToRegister">Criar Conta</button>
      </div>

      <!-- Seção direita -->
      <div class="right-section">
        <h2>INFORMAÇÕES</h2>
        <form @submit.prevent="handleLogin">
          <div class="form-group">
            <img src="https://img.icons8.com/ios-filled/50/000000/user.png" alt="user icon" />
            <input type="text" v-model="username" placeholder="Username" required />
          </div>
          <div class="form-group">
            <img src="https://img.icons8.com/ios-filled/50/000000/lock.png" alt="lock icon" />
            <input type="password" v-model="password" placeholder="Senha" required />
          </div>
          <div class="remember">
            <input type="checkbox" v-model="remember" id="remember" />
            <label for="remember">Lembrar</label>
          </div>
          <button type="submit">Entrar</button>
        </form>
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
  position: relative;
  overflow: hidden;
  background-image: url('@/assets/images/preto.jpg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
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
  background: #838282;
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

.circle.additional3 { width: 250px; 
  height: 250px; 
  bottom: 30%; 
  left: 10%; 
  background: linear-gradient(207deg, #683636 30.01%, #B8B8B8 68.84%); 
}


.logo-gabini {
  position: relative; /* Adicionei esta linha */
  z-index: 100; /* Ajuste para um valor mais alto */
}

/* Ajuste do tamanho e centralização do container */
.container {
  display: flex;
  width: 800px; /* Reduzi o tamanho da largura */
  height: 500px; /* Reduzi o tamanho da altura */
 
  background: rgba(58, 58, 58, 0.8);
  border-radius: 20px;
  overflow: hidden;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  z-index: 100;
  margin: auto;
}

/* Seção Esquerda */
.left-section {
  flex: 0.4;
  background: linear-gradient(202deg, #d9d9d9b7 42.19%, #2b2b2cb4 145%);
  color: #000;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px;
  z-index: 120;
}

.left-section h1 {
  font-size: 48px; /* Ajuste no tamanho da fonte */
  font-weight: 700;
  margin-bottom: 100px;
}

.left-section button {
  width: 200px;
  height: 40px;
  border-radius: 30px;
  background-color: #FFF;
  border: 2px solid rgba(0, 0, 0, 0.2);
  color: #000;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s ease;
}

.left-section button:hover {
  background-color: #ccc;
}

/* Seção Direita */
.right-section {
  flex: 0.6;
  background: linear-gradient(202deg, #e1e1e1b6 42.19%, #2b2b2c 145%);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 30px;
  z-index: 121;
}

.right-section h2 {
  color: #FFF;
  font-size: 36px;
  font-weight: 900;
  margin-bottom: 30px;
}

.form-group {
  width: 80%;
  margin-bottom: 15px;
  position: relative;
}

.form-group input {
  width: 100%;
  padding: 15px;
  font-size: 1rem;
  border: none;
  border-radius: 30px;
  background-color: #ddd;
  outline: none;
  padding-left: 50px;
}

.form-group img {
  position: absolute;
  top: 50%;
  left: 15px;
  transform: translateY(-50%);
  width: 20px;
  height: 20px;
}

.form-group input:focus {
  background-color: #e0e0e0;
}

.remember {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 20px;
  color: #fff;
}

.right-section button {
  width: 80%;
  padding: 15px;
  font-size: 1rem;
  background-color: #333;
  color: #fff;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.right-section button:hover {
  background-color: #444;
}

</style>
