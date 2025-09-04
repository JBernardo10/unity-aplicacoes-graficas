# Guia de Colaboração — Aplicações Gráficas e Produção de um Aplicativo

> **Contexto:** Este projeto é da disciplina **Aplicações Gráficas e Produção de um Aplicativo**.  
> O objetivo deste guia é padronizar o fluxo de trabalho e evitar perdas de código ou conflitos.

---

## Objetivos
- Garantir que **todos trabalhem a partir da versão mais recente** do projeto.
- Registrar **o que foi feito, por quem e por quê** em mensagens de commit claras.
- **Respeitar áreas de responsabilidade**: não alterar partes do projeto de outras pessoas **sem permissão**.
- Resolver conflitos **sem modificar** implementações de colegas.

---

## Fluxo de Trabalho (passo a passo)

1. **Atualize sua cópia local antes de começar**
   - Faça o **download/clone** do repositório ou execute `git pull` para trazer as versões mais recentes.
   - Verifique a branch correta (ex.: `main` para produção, `dev` para desenvolvimento, ou a sua branch pessoal).

2. **Crie/Use sua própria branch de trabalho**
> trabalhe na brach dev ou 
   - Padrão sugerido: `feat/seu-nome/descricao-curta`, `fix/seu-nome/bug-xyz`, ou `docs/seu-nome/topico`.
   - Evite trabalhar diretamente em `main`.

3. **Implemente e faça commits pequenos e frequentes**
   - A cada etapa concluída, **faça commit** com mensagem clara (ver seção *Mensagens de commit*).

4. **Antes de finalizar, sincronize novamente**
   - Dê `git pull` na branch base (ex.: `dev`) e resolva **conflitos localmente**.
   - **Não altere** o código de colegas para “fazer funcionar” sem avisá-los.

5. **Envie (upload) suas alterações**
   - Execute `git push` da sua branch.
   - Abra um **Pull Request (PR)** para a branch-alvo (ex.: `dev` → `main` quando apropriado).
   - No PR, **descreva o que foi feito** e **comente os commits** relevantes.

6. **Peça e aguarde revisão**
   - Marque colegas responsáveis pela área impactada.
   - Somente faça *merge* após a aprovação.

---

## Regras de Colaboração

- **Não mexer** em partes do projeto de outra pessoa **sem permissão explícita**.
- Se precisar alterar algo de outra área:
  - **Abra uma issue** explicando a necessidade **ou** converse com o responsável.
  - Combine a melhor abordagem (você faz, a pessoa faz, ou pair programming).
- **Evite “commits gigantes”** misturando muitas mudanças. Prefira **commits atômicos**.

---

## Mensagens de Commit (padrão sugerido)

Use um formato curto, específico e com contexto:

```
<tipo>: <resumo curto no imperativo>

Contexto/Detalhes (opcional):
- O que foi feito
- Por que foi feito
- Impactos/Dependências
```

**Tipos comuns:** `feat` (recurso), `fix` (correção), `refactor` (melhorar código), `perf` (desempenho), `style` (visual/formatação), `test` (testes), `build` (dependências), `docs` (documentação), `chore` (manutenção geral).

**Exemplos:**
- `feat: adicionar tela de login com validação de email`
- `fix: corrigir overflow no layout do dashboard`
- `docs: atualizar README com passos de execução`

---

## Resolução de Conflitos (sem alterar implementações alheias)

1. **Entenda o conflito**: veja exatamente quais trechos divergem.
2. **Preserve a intenção** de cada lado. Se não tiver certeza:
   - **Fale com a pessoa responsável** antes de “escolher” uma versão.
3. **Evite refatorar** o código do outro durante a resolução do conflito.
4. **Teste** após resolver. Se algo quebrar e depender do código de outra pessoa:
   - **Documente no PR** e **comunique** imediatamente.

---

## Checklist antes do Upload/PR

- [ ] Dei `git pull` e resolvi conflitos **localmente**.
- [ ] Não modifiquei partes que **não são minhas** sem permissão.
- [ ] Mensagens de commit **claras** e **comentários** explicando mudanças.
- [ ] Descrição do PR com **o que foi feito** e **motivo**.
- [ ] Testes e execução local **OK**.

---

## Dúvidas e Responsáveis

### Modelo de Branches do Projeto

Para organizar o desenvolvimento e evitar conflitos, o projeto terá **duas branches principais**:

#### 🔹 `main`
- Contém a **interface estável** do aplicativo.  
- Só entram alterações que já foram **testadas e aprovadas**.  
- Representa a versão “pronta” que todos podem ver.

#### 🔹 `dev`
- Contém todas as **fases do projeto em desenvolvimento**.  
- Cada membro trabalha em sua fase ou tarefa dentro da `dev`.  
- Se necessário, pode criar **branches temporárias** para tarefas específicas.  
- Quando uma fase estiver pronta → faz **merge para `dev`**.  
- Depois que todas as fases estiverem concluídas e testadas → faz **merge `dev` → `main`**.

---
> Qualquer alteração de processo deve ser combinada em equipe e registrada neste documento.

---

## TL;DR (resumo rápido)

- Sempre **baixe a versão mais recente** antes de trabalhar.  
- **Faça upload** (push) no final e **comente seus commits/PR**.  
- **Não mexa** no que é do outro **sem permissão**.  
- Em caso de conflito, **resolva sem alterar a implementação alheia** e **comunique**.



## 🔹 Termos importantes do Git

| Termo       | Significado |
|------------|-------------|
| **Branch** | Uma linha de desenvolvimento separada. Ex.: `main`, `dev`, ou uma branch de tarefa pessoal. |
| **Commit** | Um registro das alterações feitas no código. Sempre deve ter uma mensagem explicando o que foi feito. |
| **Merge**  | Combinar alterações de uma branch em outra. Ex.: `dev` → `main`. |
| **Pull**   | Trazer alterações do repositório remoto (github) para sua branch local (seu pc). Ex.: `git pull origin dev`. |
| **Push**   | Enviar suas alterações locais (seu pc) para o repositório remoto (github). Ex.: `git push origin dev`. |
| **Clone**  | Criar uma cópia local de um repositório remoto. Ex.: `git clone <URL>`. |
| **Fork**   | Copiar um repositório para sua conta, normalmente usado em projetos colaborativos externos. |

---

## 🔹 Comandos Git básicos

| Comando | Função |
|---------|--------|
| `git clone <URL>` | Clona o repositório remoto para seu computador. |
| `git status` | Mostra alterações não commitadas e a branch atual. |
| `git add <arquivo>` | Adiciona alterações ao próximo commit. |
| `git commit -m "mensagem"` | Cria um commit com suas alterações. |
| `git pull origin <branch>` | Puxa alterações do repositório remoto para sua branch local. |
| `git push origin <branch>` | Envia seus commits locais para o repositório remoto. |
| `git branch` | Lista branches existentes ou cria novas. Ex.: `git branch nova-branch`. |
| `git checkout <branch>` | Muda para outra branch. |
| `git merge <branch>` | Junta outra branch na branch atual. |

---

## 🔹 Dicas para colaboração

1. Sempre faça **pull** antes de começar a trabalhar.  
2. Trabalhe em **suas tarefas/fases na branch `dev`**.  
3. Faça **commits claros** explicando o que foi feito.  
4. Antes de enviar para o repositório remoto, teste seu código.  
5. **Merge para `main`** só depois que tudo estiver concluído e testado.  
