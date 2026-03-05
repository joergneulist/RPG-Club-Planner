<template>
  <div class="flex flex-col items-center justify-center min-h-screen bg-slate-900 text-white">
    <h1 class="text-4xl font-bold mb-8">Pen&Paper in Hofheim</h1>
    
    <div v-if="!user" class="text-center">
      <p class="mb-4">Willkommen, Wanderer. Wer seid Ihr?</p>
      <a href="/.auth/login/google?post_login_redirect_uri=/" 
         class="bg-white text-black px-6 py-3 rounded-lg font-bold hover:bg-gray-200 transition">
        Mit Google anmelden
      </a>
    </div>

    <div v-else class="max-w-md w-full p-6 bg-slate-800 rounded-xl shadow-xl">
      <h2 class="text-2xl mb-4">Willkommen, {{ user.userDetails }}</h2>
      <p v-if="isPending" class="text-yellow-400 italic">
        Du bist angemeldet. Der Wirt macht gerade Dein Zimmer bereit. Schau später noch einmal herein!
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const user = ref(null);
const isPending = ref(true);

onMounted(async () => {
  const res = await fetch('/.auth/me');
  const data = await res.json();
  user.value = data.clientPrincipal;
  
  if (user.value) {
    // Check local DB for their role
    const profile = await fetch('/api/GetMyProfile');
    const dbUser = await profile.json();
    isPending.value = (dbUser.roleName === 'Pending');
  }
});
</script>
