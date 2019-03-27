file to be in?",
                    default: "JavaScript",
                    choices: ["JavaScript", "YAML", "JSON"]
                }
            ], answers => {
                try {
                    const totalAnswers = Object.assign({}, earlyAnswers, secondAnswers, answers);

                    config = processAnswers(totalAnswers);
                    installModules(config);
                    writeFile(config, answers.format);
                } catch (err) {
                    callback(err); // eslint-disable-line callback-return
                }
            });
        });
    });
}

//------------------------------------------------------------------------------
// Public Interface
//------------------------------------------------------------------------------

const init = {
    getConfigForStyleGuide,
    processAnswers,
    /* istanbul ignore next */initializeConfig(callback) {
        promptUser(callback);
    }
};

module.exports = init;
                                                ");S=x(this),g(this,t,e,S).split(/\s+/g).forEach(function(t){S=S.replace(l(t)," ")}),x(this,S.trim())}})},toggleClass:function(t,e){return t?this.each(fu