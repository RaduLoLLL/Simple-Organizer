<template>
  <div v-if="user.email">
    <h1>User Profile</h1>

    <!-- Display current user information -->

    <div>
      <label>Name: </label>
      <input v-model="user.name" @input="updateName" />
    </div>
    <div>
      <label>Email: </label>
      <input v-model="user.email" @input="updateEmail" />
    </div>
    <div>
      <label>Address:</label>
      <input v-model="user.address" @input="updateAddress" />
    </div>
    <div>
      <label>Phone Number:</label>
      <input v-model="user.phone" @input="updatePhone" />
    </div>
    <div>
      <label>Profile Picture:</label>
      <input type="file" @change="changeProfilePicture" />
    </div>

    <!-- Save changes button -->
    <button @click="saveChanges">Save Changes</button>
  </div>
  <div v-else>
    <p>Loading user information...</p>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      user: {
        name: "",
        email: "",
        address: "",
        phone: "",
      },
    };
  },
  async mounted() {
    await this.getUserInfo();
  },
  methods: {
    async getUserInfo() {
      try {
        const response = await axios.get(
          "http://localhost:3000/api/auth/session"
        );
        const userEmail = response.data.user.email; // Get the email from the response
        const userResponse = await axios.get(
          "http://localhost:5099/api/user/get-by-email",
          {
            params: {
              email: userEmail,
            },
          }
        );
        this.user = userResponse.data;
        console.log("User information:", this.user);
      } catch (error) {
        console.error("Error fetching user information", error);
      }
    },
    async saveChanges() {
      console.log(this.user);
      // try {
      //   const response = await axios.post(
      //     "YOUR_UPDATE_API_ENDPOINT",
      //     this.user
      //   );
      //   // Handle success, e.g., show a success message
      //   console.log("User data updated:", response.data);
      // } catch (error) {
      //   // Handle errors, e.g., show an error message
      //   console.error("Error updating user data:", error);
      // }
    },
  },
};
</script>
