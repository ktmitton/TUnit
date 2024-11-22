"use strict";(self.webpackChunktunit_docs_site=self.webpackChunktunit_docs_site||[]).push([[1745],{6758:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>i,contentTitle:()=>o,default:()=>p,frontMatter:()=>r,metadata:()=>a,toc:()=>c});const a=JSON.parse('{"id":"tutorial-extras/parallel-groups","title":"Parallel Groups","description":"Parallel Groups are an alternative parallel mechanism to [NotInParallel].","source":"@site/docs/tutorial-extras/parallel-groups.md","sourceDirName":"tutorial-extras","slug":"/tutorial-extras/parallel-groups","permalink":"/TUnit/docs/tutorial-extras/parallel-groups","draft":false,"unlisted":false,"tags":[],"version":"current","sidebarPosition":11,"frontMatter":{"sidebar_position":11},"sidebar":"tutorialSidebar","previous":{"title":"Ordering Tests","permalink":"/TUnit/docs/tutorial-extras/order"},"next":{"title":"Parallel Limiter","permalink":"/TUnit/docs/tutorial-extras/parallel-limiter"}}');var s=t(4848),l=t(8453);const r={sidebar_position:11},o="Parallel Groups",i={},c=[];function u(e){const n={code:"code",h1:"h1",header:"header",p:"p",pre:"pre",...(0,l.R)(),...e.components};return(0,s.jsxs)(s.Fragment,{children:[(0,s.jsx)(n.header,{children:(0,s.jsx)(n.h1,{id:"parallel-groups",children:"Parallel Groups"})}),"\n",(0,s.jsx)(n.p,{children:"Parallel Groups are an alternative parallel mechanism to [NotInParallel]."}),"\n",(0,s.jsx)(n.p,{children:'Instead, classes that share a [ParallelGroup("")] attribute with the same key, may all run together in parallel, and nothing else will run alongside them.'}),"\n",(0,s.jsxs)(n.p,{children:["For the example below, all ",(0,s.jsx)(n.code,{children:"MyTestClass"})," tests may run in parallel, and all ",(0,s.jsx)(n.code,{children:"MyTestClass2"})," tests may run in parallel. But they should not overlap and execute both classes at the same time."]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'using TUnit.Core;\n\nnamespace MyTestProject;\n\n[ParallelGroup("Group1")]\npublic class MyTestClass\n{\n    [Test]\n    public async Task MyTest()\n    {\n        \n    }\n\n    [Test]\n    public async Task MyTest2()\n    {\n        \n    }\n\n    [Test]\n    public async Task MyTest3()\n    {\n        \n    }\n}\n\n[ParallelGroup("Group2")]\npublic class MyTestClass2\n{\n    [Test]\n    public async Task MyTest()\n    {\n        \n    }\n\n    [Test]\n    public async Task MyTest2()\n    {\n        \n    }\n\n    [Test]\n    public async Task MyTest3()\n    {\n        \n    }\n}\n'})})]})}function p(e={}){const{wrapper:n}={...(0,l.R)(),...e.components};return n?(0,s.jsx)(n,{...e,children:(0,s.jsx)(u,{...e})}):u(e)}},8453:(e,n,t)=>{t.d(n,{R:()=>r,x:()=>o});var a=t(6540);const s={},l=a.createContext(s);function r(e){const n=a.useContext(l);return a.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function o(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(s):e.components||s:r(e.components),a.createElement(l.Provider,{value:n},e.children)}}}]);