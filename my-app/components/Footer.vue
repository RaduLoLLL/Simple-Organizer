<template>
  <v-layout class="overflow-visible" style="height: 56px">
    <v-bottom-navigation v-model="value" active color="primary">
      <v-btn href="/">
        <v-icon>mdi-home</v-icon>

        Home
      </v-btn>

      <v-btn href="/my-lists">
        <v-icon> mdi-calendar-check</v-icon>

        Lists
      </v-btn>
      <v-btn href="/my-account">
        <v-icon> mdi-account</v-icon>

        My Profile
      </v-btn>
    </v-bottom-navigation>
  </v-layout>
</template>
