const CracoLessPlugin = require("craco-less-plugin");
const { getThemeVariables } = require("antd/dist/theme");

module.exports = {
    plugins: [
        {
            plugin: CracoLessPlugin,
            options: {
                modifyVars: getThemeVariables({
                    dark: false,
                    compact: true,
                    'font-size-base': '20px'
                }),
                javascriptEnabled: true,
            },
        },
    ],
};
