import { NuxtAuthHandler } from "#auth";
import GoogleProvider from "next-auth/providers/google";
import { PrismaClient } from "@prisma/client";
import axios from "axios";

export default NuxtAuthHandler({
  providers: [
    // @ts-expect-error You need to use .default here for it to work during SSR. May be fixed via Vite at some point
    GoogleProvider.default({
      scope: ["email", "profile"],
      clientId:
        "892847437429-nl57sjgmsg1et9dqj0co3pnufrnib1ja.apps.googleusercontent.com",
      clientSecret: "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",
      includeEmail: true,
    }),
  ],
  callbacks: {
    async signIn(params) {
      const prisma = new PrismaClient();
      const { user, account, profile } = params;
      if (user) {
        // Create a user object from the data
        const newUser = {
          email: user.email,
          name: user.name,
          image: user.image,
        };
        // Make a POST request to your API endpoint
        axios
          .post("http://localhost:5099/api/user/add", newUser)
          .then((response) => {
            // Handle the success response here
            console.log("User created successfully", response.data);
          })
          .catch((error) => {
            // Handle any errors here
            console.error("Error creating user", error);
          });
      }
      return true;
    },
  },
});
