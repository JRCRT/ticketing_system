const colors = require("tailwindcss/colors");

module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  darkMode: "class",
  theme: {
    colors: {
      nightPrimary: colors.green[800],
      darkPrimary: colors.green[700],
      primary: colors.green[600],
      lightPrimary: colors.green[300],

      darkSecondary: colors.gray[700],
      secondary: colors.gray[600],
      lightSecondary: colors.gray[300],

      darkSuccess: colors.green[700],
      success: colors.green[600],
      lightSuccess: colors.green[300],

      darkInfo: colors.blue[700],
      info: colors.blue[600],
      lightInfo: colors.blue[300],

      darkDanger: colors.red[700],
      danger: colors.red[600],
      lightDanger: colors.red[300],

      light: colors.white,
      light1: colors.gray[50],
      light2: colors.gray[100],
      darkText: colors.gray[200],
      dark1: colors.gray[700],
      dark2: colors.gray[800],
      dark3: colors.gray[900],

      orange: colors.yellow[500],
      blue: colors.sky[500],
      red: colors.rose[500],
      brown: colors.stone[200],

      focusActive: "hsl(218.1, 100%, 58%)",
      borderColor: "rgb(176, 176, 176)",
      transparent: "transparent",
      current: "currentColor",
      black: colors.black,
      white: colors.white,
    },
    borderColor: (theme) => ({
      ...theme("colors"),
      DEFAULT: theme("colors.lightSecondary", "currentColor"),
    }),
  },
  variants: {
    extend: {
      backgroundColor: ["checked"],
      borderColor: ["checked"],
      ringWidth: ["hover"],
    },
  },
  plugins: [],
};
