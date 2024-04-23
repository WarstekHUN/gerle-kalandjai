# UML vazlat

asfd

## teszt

asd

### Actor

A szereplők maguk

- Name: `string` *public* const
- MaxHP: `ushort` *public* const
- MaxMana:`ushort` *public* const
- Powers: `Power[]` *public*

### FightingActor

Megfogja az alap Actort és kibővíti a következőkkel:

- *private* CurrHP: `ushort`
- *private* CurrMana: `ushort`
- *private* Opponent: `ref:FightingActor`
- *public* SetOpponent(`ref:FightingActor`):
- Think(): Az NPC-k nél releváns, ez dönti el, hogy milyen támadást használjon
- Attack(kepesseg)
- DealDemage(ushort)

### Story

- Lines: `Line[]`
- isFight: `bool`

### Line

- text: `string`
- ActorRef: `ref:Actor?` Ha nincs akkor ez egy narrátor
- voiceFiles: `string?`
- zajFile: `string?`

### StoryController: *static*

- InitFight()
- PlayStory()

### ProgressController: *static*

majd

## Változók

- storytomb[]: `Story[]`
