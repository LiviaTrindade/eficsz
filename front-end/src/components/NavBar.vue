<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';

const showDropdown = ref(false);
const router = useRouter();

// Estado para o login
const isLoggedIn = ref(false);

// Função para alternar o menu dropdown (modo mobile)
function toggleDropdown() {
    showDropdown.value = !showDropdown.value;
}

// Verifica se o usuário está logado ao carregar a página
onMounted(() => {
    const userLoggedIn = localStorage.getItem('isLoggedIn');
    if (userLoggedIn === 'true') {
        isLoggedIn.value = true;
    }
});

// Função para logout
function logout() {
    localStorage.removeItem('isLoggedIn');
    isLoggedIn.value = false;
    router.push('/login');
}

// Redirecionar para a página de perfil
function goToProfile() {
    router.push('/profile');
}
</script>

<template>
    <nav class="w-[auto] h-[95px] flex-shrink-0 flex items-center justify-between px-12 navbar__styles">
        <div class="flex items-center navbar__logo">
            <img src="../assets/images/gabinipreto.png" alt="Logo" class="h-12 navbar__logo">
        </div>

        <!-- Menu normal -->
        <ul class="items__nav hidden lg:flex">
            <li class="items__menu"><a href="/" class="hover:text-blue-600">HOME</a></li>
            <li class="items__menu"><a href="#" class="hover:text-blue-600">SALE</a></li>
            <li class="items__menu"><a href="#" class="hover:text-blue-600">BUNDLE & SAVE</a></li>
            <li class="items__menu"><a href="#" class="hover:text-blue-600">SHOP BY CATEGORY</a></li>
            <li class="items__menu"><a href="#" class="hover:text-blue-600">SUPPORT</a></li>
        </ul>

        <!-- Links de login e cadastro ou perfil -->
        <div class="flex items-center space-x-4 items__nav__signin__signup hidden lg:flex">
            <template v-if="isLoggedIn">
                <!-- Link para o perfil do usuário -->
                <a href="#" @click="goToProfile" class="hover:text-blue-600 items__nav__sign">PERFIL</a>
                <button @click="logout" class="text-white py-2 px-4 items__nav__signup">LOGOUT</button>
            </template>
            <template v-else>
                <!-- Links para login e cadastro -->
                <a href="/login" class="hover:text-blue-600 items__nav__sign">SIGN IN</a>
                <div class="text__sign__signup">&nbsp;</div>
                <a href="/registration" class="text-white py-2 px-4 items__nav__signup">SIGN UP FOR FREE</a>
            </template>
        </div>

        <!-- Menu dropdown para mobile -->
        <button @click="toggleDropdown" class="block lg:hidden">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16" />
            </svg>
        </button>
    </nav>

    <!-- Dropdown menu mobile -->
    <div v-if="showDropdown" class="lg:hidden dropdown__menu">
        <ul class="dropdown__items">
            <li class="items"><a href="/" class="hover:text-blue-600">HOME</a></li>
            <li class="items"><a href="#" class="hover:text-blue-600">SALE</a></li>
            <li class="items"><a href="#" class="hover:text-blue-600">BUNDLE & SAVE</a></li>
            <li class="items"><a href="#" class="hover:text-blue-600">SHOP BY CATEGORY</a></li>
            <li class="items"><a href="#" class="hover:text-blue-600">SUPPORT</a></li>
            <template v-if="isLoggedIn">
                <li class="items"><a href="#" @click="goToProfile" class="hover:text-blue-600">PERFIL</a></li>
                <li class="items"><button @click="logout" class="hover:text-blue-600">LOGOUT</button></li>
            </template>
            <template v-else>
                <li class="items"><a href="/login" class="hover:text-blue-600">SIGN IN</a></li>
                <li class="items"><a href="/registration" class="hover:text-blue-600">SIGN UP FOR FREE</a></li>
            </template>
        </ul>
    </div>
</template>


<style scoped>
.navbar__logo {
    margin-left: 9.1%;
}

.items__nav__signin__signup {
    margin-right: 9.1%;
}

.navbar__styles {
    border: 1px solid rgba(217, 217, 227, 0.60);
    background: #FFF;
}

.navbar__logo {
    width: 135px;
    height: 26px;
    flex-shrink: 0;
    fill: #000;
}

.items__nav {
    display: inline-flex;
    padding: 0px 8px;
    justify-content: center;
    align-items: center;
    gap: 32px;
}

.items__menu {
    display: flex;
    align-items: center;
    color: var(--Black-black-500, #000);
    text-align: center;
    font-family: 'Inter', sans-serif;
    font-size: 14px;
    font-style: normal;
    font-weight: 400;
    line-height: 24px;
    letter-spacing: -0.14px;
    text-transform: uppercase;
}

.items__nav__sign {
    color: var(--Black-black-500, #000);
    text-align: center;
    font-family: 'Inter', sans-serif;
    font-size: 14px;
    font-style: normal;
    font-weight: 600;
    line-height: 24px;
    letter-spacing: -0.14px;
    text-transform: uppercase;
}

.items__nav__signup {
    color: var(--Blue-blue-500, #6987CD);
    text-align: center;
    font-family: 'Inter', sans-serif;
    font-size: 14px;
    font-style: normal;
    font-weight: 600;
    line-height: 24px;
    letter-spacing: -0.14px;
    text-transform: uppercase;
}

.dropdown__menu {
    background-color: #FFF;
    border: 1px solid rgba(217, 217, 227, 0.60);
    padding: 16px;
    transition: 3s ease-out;
}

.dropdown__items {
    display: flex;
    flex-direction: column;
    gap: 16px;
}

.text__sign__signup {
    background: #E6E6E6;
    width: 1px;
    height: 20px;
}

/* Ajustes de responsividade */
@media only screen and (max-width: 1024px) {
    .navbar__logo {
        margin-left: 0%;
    }

}

@media only screen and (max-width: 767px) {
    .navbar__logo {
        margin-left: 0%;
    }

    .items__menu {
        display: none;
    }

    /* https://renatello.com/vuejs-free-hosting/ */

}

@media only screen and (min-width: 481px) and (max-width: 767px) {
    
    .items__menu {
        display: none;
    }

}


@media only screen and (min-width: 768px) and (max-width: 1024px) {
    .items__menu {
        display: none;
    }

    .dropdown__menu {
        width: 70%;
    }

    .navbar__styles {
        width: 70%;
    }

}

@media only screen and (min-width: 820px) and (max-width: 1180px) {

    .navbar__styles {
        width: 100%;
    }
}

</style>
