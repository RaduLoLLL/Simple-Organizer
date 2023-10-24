<template>
  <div>
    <h1>My Lists</h1>

    <!-- List Selection -->
    <div class="list-selection">
      <label for="list">Select a List:</label>
      <select id="list" v-model="selectedList">
        <option v-for="(list, index) in lists" :key="index" :value="list">
          {{ list }}
        </option>
      </select>
    </div>

    <!-- Item Input Form -->
    <div class="item-form">
      <label for="item">Add an Item:</label>
      <input type="text" id="item" v-model="newItem" @keyup.enter="addItem" />
      <button @click="addItem">Add</button>
    </div>

    <!-- Display Selected List and Items -->
    <div class="selected-list">
      <h2>Selected List: {{ selectedListName }}</h2>
      <ul>
        <li v-for="item in items" :key="item.id">
          {{ item.content }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      lists: [],
      selectedList: null,
      newItem: "",
      items: [],
    };
  },
  computed: {
    selectedListName() {
      const selectedList = this.lists.find(
        (list) => list === this.selectedList
      );
      return selectedList ? selectedList : "No List Selected";
    },
    selectedListItems() {
      return this.items.filter((item) => item.listId === this.selectedList);
    },
  },
  methods: {
    fetchLists() {
      // Fetch the lists from the API endpoint
      axios
        .get("http://localhost:5099/api/items/list-types")
        .then((response) => {
          this.lists = response.data;
        })
        .catch((error) => {
          console.error("Error fetching lists:", error);
        });
    },
    async addItem() {
      if (this.newItem && this.selectedList) {
        const response = await axios.get(
          "http://localhost:3000/api/auth/session"
        );

        const userEmail = response.data.user.email; // Get the email from the response
        axios
          .get(`http://localhost:5099/api/user/get-by-email?email=${userEmail}`)
          .then((response) => {
            const userId = response.data.id;
            const newItem = {
              content: this.newItem,
              list: this.selectedListName,
              user_id: userId, // Replace with the actual user ID
            };
            axios
              .post("http://localhost:5099/api/items/add-item", newItem)
              .then((response) => {
                console.log("Item added successfully");
                this.newItem = ""; // Clear the input field
                this.fetchUserItems(); // Fetch updated item list
                // Refresh the list of items or perform other necessary actions
              })
              .catch((error) => {
                console.error("Error adding item:", error);
              });
          });
      }
    },
    async fetchUserItems() {
      if (this.selectedList) {
        // Fetch the user ID based on the user's email
        const response = await axios.get(
          "http://localhost:3000/api/auth/session"
        );

        const userEmail = response.data.user.email; // Get the email from the response
        axios
          .get(`http://localhost:5099/api/user/get-by-email?email=${userEmail}`)
          .then((response) => {
            const userId = response.data.id;
            const listType = this.selectedListName;

            // Fetch the items based on the user ID and selected list type
            axios
              .get(
                `http://localhost:5099/api/items/items?userId=${userId}&listType=${listType}`
              )
              .then((itemsResponse) => {
                this.items = itemsResponse.data;
              })
              .catch((itemsError) => {
                console.error("Error fetching items:", itemsError);
              });
          })
          .catch((userError) => {
            console.error("Error fetching user:", userError);
          });
      }
    },
  },
  created() {
    this.fetchLists();
    axios
      .get("http://localhost:5099/api/items/list-types")
      .then((response) => {
        this.lists = response.data;
      })
      .catch((error) => {
        console.error("Error fetching lists:", error);
      });
  },
  watch: {
    selectedList: "fetchUserItems",
  },
};
</script>

<style scoped>
/* Add your CSS styling here */
</style>
