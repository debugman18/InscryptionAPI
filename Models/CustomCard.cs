using System.Collections.Generic;
using CardLoaderPlugin.lib;
using DiskCardGame;
using UnityEngine;

namespace APIPlugin
{
	public class CustomCard
	{
		public static List<CustomCard> cards = new List<CustomCard>();
		public static Dictionary<int,List<AbilityIdentifier>> abilityIds = new Dictionary<int,List<AbilityIdentifier>>();
		public static Dictionary<int,EvolveIdentifier> evolveIds = new Dictionary<int,EvolveIdentifier>();
		public static Dictionary<int,IceCubeIdentifier> iceCubeIds = new Dictionary<int,IceCubeIdentifier>();
		public static Dictionary<int,TailIdentifier> tailIds = new Dictionary<int,TailIdentifier>();
		public string name;
		public List<CardMetaCategory> metaCategories;
		public CardComplexity? cardComplexity;
		public CardTemple? temple;
		public string displayedName;
		public int? baseAttack;
		public int? baseHealth;
		public string description;
		public bool? hideAttackAndHealth;
		public int? cost;
		public int? bonesCost;
		public int? energyCost;
		public List<GemType> gemsCost;
		public SpecialStatIcon? specialStatIcon;
		public List<Tribe> tribes;
		public List<Trait> traits;
		public List<SpecialTriggeredAbility> specialAbilities;
		public List<Ability> abilities;
		public EvolveParams evolveParams;
		public string defaultEvolutionName;
		public TailParams tailParams;
		public IceCubeParams iceCubeParams;
		public bool? flipPortraitForStrafe;
		public bool? onePerDeck;
		public List<CardAppearanceBehaviour.Appearance> appearanceBehaviour;
		[IgnoreMapping]
		public Texture2D tex;
		[IgnoreMapping]
		public Texture2D altTex;
		public Texture titleGraphic;
		[IgnoreMapping]
		public Texture2D pixelTex;
		public GameObject animatedPortrait;
		public List<Texture> decals;
		public List<AbilityIdentifier> abilityId;
		public EvolveIdentifier evolveId;
		public IceCubeIdentifier iceCubeId;
		public TailIdentifier tailId;

		public CustomCard(string name, List<AbilityIdentifier> abilityId=null, EvolveIdentifier evolveId=null, IceCubeIdentifier iceCubeId=null, TailIdentifier tailId=null)
		{
			this.name = name;
			CustomCard.cards.Add(this);

			// Handle AbilityIdentifier
			List<AbilityIdentifier> toRemove = new List<AbilityIdentifier>();
			if (this.abilityId is not null)
			{
				foreach (AbilityIdentifier id in abilityId)
				{
					if (id.id != 0)
					{
						this.abilities.Add(id.id);
					}
				}
				foreach (AbilityIdentifier id in toRemove)
				{
					this.abilityId.Remove(id);
				}
			}
			if (abilityId is not null && this.abilityId.Count > 0)
			{
				CustomCard.abilityIds[CustomCard.cards.Count - 1] = abilityId;
			}

			// Handle EvolveIdentifier
			if (evolveId is not null)
			{
				CustomCard.evolveIds[CustomCard.cards.Count - 1] = evolveId;
			}

			// Handle IceCubeIdentifier
			if (iceCubeId is not null)
			{
				CustomCard.iceCubeIds[CustomCard.cards.Count - 1] = iceCubeId;
			}

			// Handle TailIdentifier
			if (tailId is not null)
			{
				CustomCard.tailIds[CustomCard.cards.Count - 1] = tailId;
			}
		}

		public CardInfo AdjustCard(CardInfo card)
		{
			TypeMapper<CustomCard, CardInfo>.Convert(this, card);

			if (this.tex is not null)
			{
				tex.name = "portrait_" + name;
				tex.filterMode = FilterMode.Point;
				card.portraitTex = Sprite.Create(tex, CardUtils.DefaultCardArtRect, CardUtils.DefaultVector2);
				card.portraitTex.name = "portrait_" + name;
			}
			if (this.altTex is not null)
			{
				altTex.name = "portrait_" + name;
				altTex.filterMode = FilterMode.Point;
				card.alternatePortrait = Sprite.Create(altTex, CardUtils.DefaultCardArtRect, CardUtils.DefaultVector2);
				card.alternatePortrait.name = "portrait_" + name;
			}
			if (this.pixelTex is not null)
			{
				pixelTex.name = "portrait_" + name;
				pixelTex.filterMode = FilterMode.Point;
				card.pixelPortrait = Sprite.Create(pixelTex, CardUtils.DefaultCardPixelArtRect, CardUtils.DefaultVector2);
				card.pixelPortrait.name = "portrait_" + name;
			}
			Plugin.Log.LogInfo($"Adjusted default card {name}!");
			return card;
		}
	}
}