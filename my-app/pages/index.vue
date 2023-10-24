<template class="">
  <div class="d-flex flex-column justify-center align-center h-screen">
    <p v-if="status">{{ status }}</p>
    <span v-if="data">Salut {{ data.user.name }}</span>
    <v-btn
      v-if="!data"
      @click="loginWithGithub"
      prepend-icon="mdi-login-variant"
      class="my-6"
      >Login with third party provider</v-btn
    >
    <v-btn v-if="data" @click="logout" prepend-icon="mdi-logout" class="my-2"
      >Sign out</v-btn
    >
    <form v-if="!data" @submit.prevent="submit" class="w-25">
      <v-text-field
        v-model="username.value.value"
        :counter="10"
        :error-messages="username.errorMessage.value"
        label="Username"
      ></v-text-field>

      <v-text-field
        v-model="password.value.value"
        :counter="7"
        :error-messages="password.errorMessage.value"
        type="password"
        label="Password"
      ></v-text-field>

      <v-btn class="me-4" type="submit"> submit </v-btn>

      <v-btn @click="handleReset"> clear </v-btn>
    </form>
  </div>
</template>
<script setup>
import { useField, useForm } from "vee-validate";
const { status, data, signIn, signOut } = useAuth();

const loginWithGithub = () => {
  signIn();
};
const logout = () => {
  signOut();
};

const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    username(value) {
      if (value?.length >= 2) return true;

      return "Name needs to be at least 2 characters.";
    },
    password(value) {
      if (value?.length > 8) return true;

      return "Password needs to be at least 8 chars long.";
    },
  },
});
const username = useField("username");
const password = useField("password");

const submit = handleSubmit((values) => {
  alert(JSON.stringify(values, null, 2));
});
definePageMeta({
  middleware: "global",
});
</script>
