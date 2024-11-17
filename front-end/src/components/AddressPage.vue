<template>
    <div class="address-container">
      <h2><span>Endereço</span> de Entrega</h2>
      <form @submit.prevent="submitAddress">
        <div class="input-group">
          <input type="text" v-model="address.rua" placeholder="Rua" />
        </div>
        <div class="input-group">
          <input type="text" v-model="address.numero" placeholder="Número" />
        </div>
        <div class="input-group">
          <input type="text" v-model="address.bairro" placeholder="Bairro" />
        </div>
        <div class="input-group">
          <input type="text" v-model="address.cidade" placeholder="Cidade" />
        </div>
        <div class="input-group">
          <input type="text" v-model="address.estado" placeholder="Estado" />
        </div>
        <div class="input-group">
          <input type="text" v-model="address.cep" placeholder="CEP" />
        </div>
        <button type="submit" class="save-button">Salvar Endereço</button>
      </form>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import axios from 'axios';
  
  const router = useRouter();
  const address = ref({
    rua: '',
    numero: '',
    bairro: '',
    cidade: '',
    estado: '',
    cep: ''
  });
  
  const submitAddress = async () => {
    try {
      await axios.post('https://672017f0e7a5792f053074c2.mockapi.io/apa/address', address.value);
      router.push('/success'); // Redirecionar após salvar
    } catch (error) {
      console.error('Erro ao salvar endereço:', error);
    }
  };
  </script>
  
  <style scoped>
  .address-container {
    width: 100%;
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
    background-color: #333;
    color: #fff;
    border-radius: 10px;
  }
  
  h2 {
    text-align: center;
    font-size: 24px;
    margin-bottom: 20px;
  }
  
  .input-group {
    margin-bottom: 15px;
  }
  
  .input-group input {
    width: 100%;
    padding: 10px;
    border-radius: 50px;
    border: 1px solid #555;
    background-color: #444;
    color: #fff;
  }
  
  .save-button {
    width: 100%;
    padding: 12px;
    background-color: #03B1FF;
    border: none;
    color: white;
    cursor: pointer;
    border-radius: 50px;
  }
  
  .save-button:hover {
    background-color: #007ACC;
  }
  </style>
  